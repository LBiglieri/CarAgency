using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Error cuyo texto puede traducirse en la capa de presentacion.
    /// El dominio expone una clave y sus argumentos; nunca el texto final.
    /// </summary>
    public interface ITranslatableError
    {
        string TranslationKey { get; }
        object[] TranslationArguments { get; }
    }
}
