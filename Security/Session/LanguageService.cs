using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Windows.Forms;
using BE;
using CarAgency.BE;

namespace Security.Session
{
    public static class LanguageService
    {
        private static Dictionary<string, string> translations = new Dictionary<string, string>();
        private static LanguageManager languageManager = new LanguageManager();
        public static void LoadLanguage(string language)
        {
            translations.Clear();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load($"{language}.xml");
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/translations/translation");
            foreach (XmlNode node in nodeList)
            {
                string tag = node.Attributes["tag"].Value;
                string text = node.InnerText;
                translations[tag] = text;
            }
            languageManager.Notify(language);
        }
        public static string GetTagText(string tag)
        {
            if (translations.TryGetValue(tag, out string text))
                return text;
            else
                return "";
        }
        public static string GetTagText(string tag, string fallback)
        {
            string text = GetTagText(tag);
            return string.IsNullOrEmpty(text) ? fallback : text;
        }
        /// <summary>
        /// Traduce un error de negocio. Si la excepcion no declara clave de traduccion
        /// o la clave no existe en el idioma actual, devuelve el Message original.
        /// </summary>
        public static string GetErrorText(Exception error)
        {
            if (error == null) return "";
            ITranslatableError translatable = error as ITranslatableError;
            if (translatable == null) return error.Message;
            string text = GetTagText(translatable.TranslationKey);
            if (string.IsNullOrEmpty(text)) return error.Message;
            object[] arguments = translatable.TranslationArguments;
            if (arguments == null || arguments.Length == 0) return text;
            try
            {
                return string.Format(text, arguments);
            }
            catch (FormatException)
            {
                return error.Message;
            }
        }
        public static void Attach(ILanguageObserver observer)
        {
            languageManager.Attach(observer);
        }
        public static void Detach(ILanguageObserver observer)
        {
            languageManager.Detach(observer);
        }
        public static string GetCurrentLanguage()
        {
            return languageManager.GetCurrentLanguage();
        }

        public static List<Languages> GetAvailableLanguages()
        {
            return new List<Languages>
            {
                new Languages("en", "English"),
                new Languages("es", "Español"),
                new Languages("po", "Português")
            };
        }

    }
}
