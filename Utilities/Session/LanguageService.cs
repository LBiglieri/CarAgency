using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Windows.Forms;
using Entities;

namespace Utilities.Session
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

    }
}
