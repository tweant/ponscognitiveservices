using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServicesWPF.Model.Language
{
    public class Language
    {
        public string Name { get; }
        public string Warning { get; private set; }

        /// <summary>
        /// ISO 639-1 Code
        /// </summary>
        public string PonsCode { get; }

        public List<SpeechLocale> SpeechLocales { get; }
        public SpeechLocale CurrentLocale { get; private set; }
        public bool IsPonsAvailable { get; private set; }
        public bool IsMicrosoftSpeechRecognitionAvailable { get; private set; }
        public List<Translation> TranslatesTo { get; }

        public Language(string name, string ponsCode,
            List<SpeechLocale> speechLocales, string warning=null, bool isPonsAvailable = true)
        {
            if (ponsCode!=null && ponsCode.Length != 2)
                throw new Exception("PonsCode should consists only of two characters. Check ISO 639-1 specification.");
            Name = name;
            Warning = warning;
            PonsCode = ponsCode;
            IsPonsAvailable = isPonsAvailable;
            TranslatesTo = new List<Translation>();

            if (speechLocales == null || speechLocales.Count == 0)
            {
                SpeechLocales = null;
                CurrentLocale = null;
                IsMicrosoftSpeechRecognitionAvailable = false;
            }
            else
            {
                SpeechLocales = speechLocales;
                CurrentLocale = SpeechLocales[0];
                IsMicrosoftSpeechRecognitionAvailable = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localeCode">Locale string in format xx-XX</param>
        /// <returns>Boolean indicating whether current locale was successfully changed or not.</returns>
        public bool SetCurrentLocale(string localeCode)
        {
            SpeechLocale locale = SpeechLocales.Find(x => x.Code.Equals(localeCode));
            if (locale == null)
            {
                return false;
            }
            CurrentLocale = locale;
            return true;
        }

        public void AddTranslationLanguage(IEnumerable<KeyValuePair<Language,string>> translationPair)
        {
            TranslatesTo.AddRange(translationPair.Select(x=>new Translation(this,x.Key,x.Value)));
        }

        //public override bool Equals(Object obj)
        //{
        //    // Check for null values and compare run-time types.
        //    if (obj == null || GetType() != obj.GetType())
        //        return false;

        //    return this.PonsCode.Equals(((Language)obj).PonsCode);
        //}

        //public override int GetHashCode()
        //{
        //    return Name.GetHashCode() ^ PonsCode.GetHashCode();
        //}
    }
}