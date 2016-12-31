using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServicesWPF.Model.Language
{
    public class Translation
    {
        /// <summary>
        /// Format xxxx (e.g. depl)
        /// </summary>
        public string DictionaryCode { get; set; }
        public Language LanguageFrom { get; set; }
        public Language LanguageTo { get; set; }

        public Translation(Language languageFrom, Language languageTo, string dictionaryCode)
        {
            DictionaryCode = dictionaryCode;
            LanguageFrom = languageFrom;
            LanguageTo = languageTo;
        }
    }
}
