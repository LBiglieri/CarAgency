using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Session
{
    public class LanguageManager: ILanguageSubject
    {
        private List<ILanguageObserver> observers = new List<ILanguageObserver>();
        private string currentLanguage;

        public void Attach(ILanguageObserver observer)
        {
            observers.Add(observer);
        }
        public void Detach(ILanguageObserver observer)
        {
            observers.Remove(observer);
        }
        public void Notify(string language)
        {
            currentLanguage = language;
            foreach (var observer in observers)
            {
                observer.UpdateLanguage(language);
            }
        }
        public string GetCurrentLanguage()
        {
            return currentLanguage;
        }

    }
}
