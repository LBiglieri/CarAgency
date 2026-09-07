using System;
using System.Configuration;

namespace CarAgency.Security.Integrity
{
    public static class DigitVerifierKey
    {
        public const string SettingName = "CARAGENCY_DV_KEY";

        public static string ConfigurationFilePath
        {
            get { return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile; }
        }

        public static byte[] Load()
        {
            string configured = ReadConfiguration();
            if (string.IsNullOrWhiteSpace(configured))
                throw new InvalidOperationException("Falta configurar " + SettingName
                    + " en las variables de entorno o en appSettings de " + ConfigurationFilePath
                    + ". Si la base ya tiene digitos, use la clave original.");

            byte[] key;
            try
            {
                key = Convert.FromBase64String(configured);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException(SettingName + " debe contener una clave de 32 bytes codificada en Base64.");
            }
            if (key.Length != 32)
            {
                Array.Clear(key, 0, key.Length);
                throw new InvalidOperationException(SettingName + " debe contener una clave de 32 bytes codificada en Base64.");
            }
            return key;
        }

        private static string ReadConfiguration()
        {
            string configured = Environment.GetEnvironmentVariable(SettingName);
            return string.IsNullOrWhiteSpace(configured)
                ? ConfigurationManager.AppSettings[SettingName]
                : configured;
        }
    }
}
