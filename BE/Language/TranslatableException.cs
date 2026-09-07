using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Excepcion de negocio que viaja con la clave de traduccion en lugar del texto final.
    /// El Message queda en espanol como respaldo para logs y para cuando falta la traduccion.
    /// </summary>
    public class TranslatableException : Exception, ITranslatableError
    {
        public string TranslationKey { get; private set; }
        public object[] TranslationArguments { get; private set; }

        public TranslatableException(string translationKey, string fallback, params object[] arguments)
            : base(Build(fallback, arguments))
        {
            TranslationKey = translationKey;
            TranslationArguments = arguments ?? new object[0];
        }

        public TranslatableException(string translationKey, string fallback, Exception innerException, params object[] arguments)
            : base(Build(fallback, arguments), innerException)
        {
            TranslationKey = translationKey;
            TranslationArguments = arguments ?? new object[0];
        }

        private static string Build(string fallback, object[] arguments)
        {
            if (string.IsNullOrEmpty(fallback)) return "";
            if (arguments == null || arguments.Length == 0) return fallback;
            try
            {
                return string.Format(fallback, arguments);
            }
            catch (FormatException)
            {
                return fallback;
            }
        }
    }
}
