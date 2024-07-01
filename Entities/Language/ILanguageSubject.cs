using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface ILanguageSubject
    {
        void Attach(ILanguageObserver observer);
        void Detach(ILanguageObserver observer);
        void Notify(string language);

    }
}
