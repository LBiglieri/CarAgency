using System;
using System.Collections.Generic;
using CarAgency.BE.Integrity;
using BE;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace CarAgency.Security.Integrity
{
    public sealed class DigitVerifierCalculator
    {
        private readonly byte[] key;
        private static readonly Encoding utf8 = new UTF8Encoding(false, true);
        public string KeyId { get; private set; }

        public DigitVerifierCalculator(byte[] key)
        {
            if (key == null || key.Length != 32) throw new ArgumentException("Se requiere una clave de 32 bytes.", nameof(key));
            this.key = (byte[])key.Clone();
            using (var sha = SHA256.Create()) KeyId = Hex(sha.ComputeHash(key));
        }

        public string CalculateDvh(string table, IReadOnlyList<DVColumn> columns, Func<string, object> getValue)
        {
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, utf8, true))
            {
                writer.Write("CarAgency.DV.v1");
                writer.Write("DVH");
                writer.Write("dbo." + table);
                writer.Write(columns.Count);
                foreach (DVColumn column in columns)
                {
                    string name = column.Name;
                    string type = column.SqlType;
                    writer.Write(name);
                    writer.Write(type);
                    object value = Normalize(column, getValue(name));
                    bool isNull = value == null;
                    writer.Write(isNull);
                    if (isNull) continue;
                    switch (type)
                    {
                        case "varchar": case "nvarchar": writer.Write((string)value); break;
                        case "uniqueidentifier": writer.Write(((Guid)value).ToByteArray()); break;
                        case "int": writer.Write((int)value); break;
                        case "bit": writer.Write((bool)value); break;
                        case "float": writer.Write((double)value); break;
                        case "datetime": writer.Write(((DateTime)value).Ticks); break;
                        default: throw new NotSupportedException("Tipo no soportado para DVH: " + type);
                    }
                }
                writer.Flush();
                using (var hmac = new HMACSHA256(key)) return Hex(hmac.ComputeHash(buffer.ToArray()));
            }
        }

        public string CalculateDvv(string table, IEnumerable<string> horizontalDigests)
        {
            BigInteger sum = BigInteger.Zero;
            foreach (string digest in horizontalDigests)
            {
                if (!IsDigest(digest)) throw new TranslatableException("DVDigestMalformed", "DVH ausente o mal formado en dbo.{0}", table);
                // El cero inicial evita que BigInteger interprete el bit mas alto como signo.
                sum += BigInteger.Parse("0" + digest, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            }
            using (var buffer = new MemoryStream())
            using (var writer = new BinaryWriter(buffer, utf8, true))
            {
                writer.Write("CarAgency.DV.v1");
                writer.Write("DVV");
                writer.Write("dbo." + table);
                byte[] total = sum.ToByteArray(); // Entero exacto, little endian, con signo positivo.
                writer.Write(total.Length);
                writer.Write(total);
                writer.Flush();
                using (var hmac = new HMACSHA256(key)) return Hex(hmac.ComputeHash(buffer.ToArray()));
            }
        }

        public static object Normalize(DVColumn column, object value)
        {
            string name = column.Name;
            if (value == null)
            {
                if (!column.IsNullable) throw new ArgumentException("La columna " + name + " no admite NULL.");
                return null;
            }
            switch (column.SqlType)
            {
                case "varchar": case "nvarchar":
                    string text = Convert.ToString(value, CultureInfo.InvariantCulture);
                    int limit = column.MaxLength;
                    int length;
                    if (column.SqlType == "varchar")
                    {
                        int codePage = column.CodePage;
                        // Rechazar caracteres no representables evita cambios silenciosos al persistir.
                        length = Encoding.GetEncoding(codePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetBytes(text).Length;
                    }
                    else
                    {
                        utf8.GetByteCount(text); // Rechaza sustitutos Unicode invalidos.
                        length = checked(text.Length * 2);
                    }
                    if (limit >= 0 && length > limit) throw new ArgumentException("El valor excede el largo de " + name + ".");
                    return text;
                case "uniqueidentifier": return value is Guid ? value : Guid.Parse(Convert.ToString(value, CultureInfo.InvariantCulture));
                case "int": return Convert.ToInt32(value, CultureInfo.InvariantCulture);
                case "bit": return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
                case "datetime": return new SqlDateTime(Convert.ToDateTime(value, CultureInfo.InvariantCulture)).Value;
                case "float":
                    double number = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                    if (double.IsNaN(number) || double.IsInfinity(number)) throw new ArgumentException("Numero no persistible en " + name + ".");
                    return number == 0 ? 0d : number;
                default: throw new NotSupportedException("Tipo no soportado para DVH: " + column.SqlType);
            }
        }

        public static bool IsDigest(string value)
        {
            if (value == null || value.Length != 64) return false;
            foreach (char c in value) if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))) return false;
            return true;
        }

        private static string Hex(byte[] bytes) { return BitConverter.ToString(bytes).Replace("-", ""); }
    }
}
