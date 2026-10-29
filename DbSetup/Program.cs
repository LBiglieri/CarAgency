using System;
using System.Collections.Generic;
using System.IO;

namespace CarAgency.DbSetup
{
    // Uso:
    //   DbSetup check   --server <instancia> [--database CarAgency]
    //   DbSetup install --server <instancia> --script <archivo.sql> [--database CarAgency] [--replace] [--config <UI.exe.config>] [--log <archivo>]
    //
    // check:   0 = la base no existe, 1 = la base existe, 2 = error (por ejemplo, no se pudo conectar).
    // install: 0 = ok, 2 = error. Si la base existe y no se pasa --replace, se conserva intacta (reparacion).
    public static class Program
    {
        public const int Ok = 0;
        public const int DatabaseExists = 1;
        public const int Failure = 2;

        private const string DefaultDatabase = "CarAgency";
        private static string logPath;

        public static int Main(string[] args)
        {
            Dictionary<string, string> options;
            string command;
            try
            {
                command = args.Length > 0 ? args[0].ToLowerInvariant() : "";
                options = Parse(args);
                options.TryGetValue("log", out logPath);
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return Failure;
            }

            try
            {
                switch (command)
                {
                    case "check": return Check(options);
                    case "install": return Install(options);
                    default:
                        Console.Error.WriteLine("Comando desconocido. Use 'check' o 'install'.");
                        return Failure;
                }
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                return Failure;
            }
        }

        private static int Check(Dictionary<string, string> options)
        {
            DatabaseInstaller installer = CreateInstaller(options);
            return installer.Exists() ? DatabaseExists : Ok;
        }

        private static int Install(Dictionary<string, string> options)
        {
            DatabaseInstaller installer = CreateInstaller(options);
            bool replace = options.ContainsKey("replace");
            bool exists = installer.Exists();

            if (exists && !replace)
            {
                Log("La base ya existe y no se pidio reemplazarla: se conserva sin cambios.");
            }
            else
            {
                string scriptPath = Require(options, "script");
                IReadOnlyList<SqlBatch> batches = SqlScript.Load(scriptPath);
                Log("Script " + scriptPath + ": " + batches.Count + " lotes.");

                if (exists)
                {
                    installer.Backup();
                    installer.Drop();
                }
                installer.Create();
                try
                {
                    installer.Run(batches);
                }
                catch
                {
                    // Una base a medio crear haria que la proxima instalacion la detecte como existente.
                    Log("Se elimina la base incompleta.");
                    installer.Drop();
                    throw;
                }
            }

            string configPath;
            if (options.TryGetValue("config", out configPath))
            {
                AppConfigWriter.SetConnectionString(configPath, DefaultDatabase, installer.ApplicationConnectionString);
                Log("Cadena de conexion actualizada en " + configPath + ".");
            }
            return Ok;
        }

        private static DatabaseInstaller CreateInstaller(Dictionary<string, string> options)
        {
            string database;
            if (!options.TryGetValue("database", out database)) database = DefaultDatabase;
            return new DatabaseInstaller(Require(options, "server"), database, Log);
        }

        private static Dictionary<string, string> Parse(string[] args)
        {
            var options = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 1; i < args.Length; i++)
            {
                if (!args[i].StartsWith("--")) throw new ArgumentException("Argumento inesperado: " + args[i]);
                string name = args[i].Substring(2);
                bool isFlag = i + 1 >= args.Length || args[i + 1].StartsWith("--");
                options[name] = isFlag ? "" : args[++i];
            }
            return options;
        }

        private static string Require(Dictionary<string, string> options, string name)
        {
            string value;
            if (!options.TryGetValue(name, out value) || value.Length == 0)
                throw new ArgumentException("Falta el parametro --" + name + ".");
            return value;
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
            if (string.IsNullOrEmpty(logPath)) return;
            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + message + Environment.NewLine);
        }
    }
}
