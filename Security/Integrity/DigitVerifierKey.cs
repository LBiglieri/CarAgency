using System;
using System.Configuration;
using BE;

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
                throw new TranslatableException("DVKeySettingMissing",
                    "Falta configurar {0} en las variables de entorno o en appSettings de {1}. Si la base ya tiene digitos, use la clave original.",
                    SettingName, ConfigurationFilePath);

            byte[] key;
            try
            {
                key = Convert.FromBase64String(configured);
            }
            catch (FormatException)
            {
                throw new TranslatableException("DVKeySettingInvalid", "{0} debe contener una clave de 32 bytes codificada en Base64.", SettingName);
            }
            if (key.Length != 32)
            {
                Array.Clear(key, 0, key.Length);
                throw new TranslatableException("DVKeySettingInvalid", "{0} debe contener una clave de 32 bytes codificada en Base64.", SettingName);
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
