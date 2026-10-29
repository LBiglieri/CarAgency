using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CarAgency.DbSetup
{
    public sealed class SqlBatch
    {
        public SqlBatch(string text, int firstLine, int repeat)
        {
            Text = text;
            FirstLine = firstLine;
            Repeat = repeat;
        }

        public string Text { get; }
        public int FirstLine { get; }
        public int Repeat { get; }
    }

    // GO no es T-SQL: lo interpretan sqlcmd y SSMS. Aca se separan los lotes igual que ellos,
    // ignorando los GO que caen dentro de strings, identificadores entre corchetes o comentarios de bloque.
    public static class SqlScript
    {
        private static readonly Regex separator = new Regex(@"^\s*GO(?:\s+(\d+))?\s*(?:--.*)?$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        public static IReadOnlyList<SqlBatch> Load(string path)
        {
            // StreamReader detecta el BOM: SSMS guarda los scripts en UTF-16 por defecto.
            using (var reader = new StreamReader(path, Encoding.UTF8, true)) return Split(reader.ReadToEnd());
        }

        public static IReadOnlyList<SqlBatch> Split(string script)
        {
            var batches = new List<SqlBatch>();
            var current = new StringBuilder();
            var state = new ScanState();
            int firstLine = 1;
            // Cada linea conserva su '\r': el lote se envia con los fines de linea originales, como hace sqlcmd,
            // para que el texto de los procedimientos quede igual al de la base de origen.
            string[] lines = script.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                Match match = state.IsInsideLiteral ? Match.Empty : separator.Match(line);
                if (match.Success)
                {
                    int repeat = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 1;
                    Add(batches, current, firstLine, repeat);
                    firstLine = i + 2;
                    continue;
                }
                if (current.Length == 0 && line.Trim().Length == 0) { firstLine = i + 2; continue; }
                current.Append(line);
                if (i < lines.Length - 1) current.Append('\n');
                state.Scan(line);
            }
            Add(batches, current, firstLine, 1);
            return batches;
        }

        private static void Add(List<SqlBatch> batches, StringBuilder current, int firstLine, int repeat)
        {
            string text = current.ToString();
            current.Clear();
            if (text.Trim().Length > 0 && repeat > 0) batches.Add(new SqlBatch(text, firstLine, repeat));
        }

        private sealed class ScanState
        {
            private bool inString;
            private bool inBracket;
            private int commentDepth;

            public bool IsInsideLiteral
            {
                get { return inString || inBracket || commentDepth > 0; }
            }

            public void Scan(string line)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    char next = i + 1 < line.Length ? line[i + 1] : '\0';

                    if (commentDepth > 0)
                    {
                        if (c == '*' && next == '/') { commentDepth--; i++; }
                        else if (c == '/' && next == '*') { commentDepth++; i++; }
                    }
                    else if (inString)
                    {
                        // '' es una comilla escapada dentro del string.
                        if (c == '\'' && next == '\'') i++;
                        else if (c == '\'') inString = false;
                    }
                    else if (inBracket)
                    {
                        if (c == ']' && next == ']') i++;
                        else if (c == ']') inBracket = false;
                    }
                    else if (c == '-' && next == '-') return;
                    else if (c == '/' && next == '*') { commentDepth++; i++; }
                    else if (c == '\'') inString = true;
                    else if (c == '[') inBracket = true;
                }
            }
        }
    }
}
