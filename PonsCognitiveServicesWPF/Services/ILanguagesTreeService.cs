using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PonsCognitiveServicesWPF.Model.Language;

namespace PonsCognitiveServicesWPF.Services
{
    public interface ILanguagesTreeService
    {
        List<Language> LanguageTree { get; }
        bool SetLanguageLocale(string languageCode, string languageLocale);
    }
}
