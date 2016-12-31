using System;
using System.Collections.Generic;
using PonsCognitiveServicesWPF.Model.Language;

namespace PonsCognitiveServicesWPF.Services
{
    public class LanguagesTreeService : ILanguagesTreeService
    {
        public List<Language> LanguageTree { get; }

        public LanguagesTreeService()
        {
            LanguageTree = new List<Language>();
            //PONS + COGNITIVE SERVICES
            var arabic = new Language("Arabic", "ar",
                new List<SpeechLocale> {new SpeechLocale("Arabic (Egypt)", "ar-EG", true)},
                "ar-EG supports Modern Standard Arabic (MSA)");
            var danish = new Language("Danish", "da",
                new List<SpeechLocale> {new SpeechLocale("Danish", "da-DK", false)});
            var german = new Language("German", "de",
                new List<SpeechLocale> {new SpeechLocale("German", "de-DE", true)});
            var english = new Language("English", "en",
                new List<SpeechLocale>
                {
                    new SpeechLocale("English (U.K.)", "en-GB", true),
                    new SpeechLocale("English (Australia)", "en-AU", true),
                    new SpeechLocale("English (Canada)", "en-CA", true),
                    new SpeechLocale("English (India)", "en-IN", true),
                    new SpeechLocale("English (New Zealand)", "en-NZ", false),
                    new SpeechLocale("English (U.S.)", "en-US", true)
                });
            var spanish = new Language("Spanish", "es",
                new List<SpeechLocale>
                {
                    new SpeechLocale("Spanish", "es-ES", true),
                    new SpeechLocale("Spanish (Mexican)", "es-MX", true)
                });
            var french = new Language("French", "fr",
                new List<SpeechLocale>
                {
                    new SpeechLocale("French (Canadian)", "fr-CA", true),
                    new SpeechLocale("French", "fr-FR", true)
                });
            var italian = new Language("Italian", "it",
                new List<SpeechLocale> {new SpeechLocale("Italian", "it-IT", true)});
            var norwegian = new Language("Norwegian", "no",
                new List<SpeechLocale> {new SpeechLocale("Norwegian (Bokmal)", "nb-NO", false)});
            var dutch = new Language("Dutch", "nl",
                new List<SpeechLocale> {new SpeechLocale("Dutch", "nl-NL", false)});
            var polish = new Language("Polish", "pl",
                new List<SpeechLocale> {new SpeechLocale("Polish", "pl-PL", false)});
            var portuguese = new Language("Portuguese", "pt",
                new List<SpeechLocale>
                {
                    new SpeechLocale("Portuguese", "pt-PT", false),
                    new SpeechLocale("Portuguese (Brazilian)", "pt-BR", true)
                });
            var russian = new Language("Russian", "ru",
                new List<SpeechLocale> {new SpeechLocale("Russian", "ru-RU", true)});
            var swedish = new Language("Swedish", "sv",
                new List<SpeechLocale> {new SpeechLocale("Swedish", "sv-SE", false)});
            var chinese = new Language("Chinese", "zh",
                new List<SpeechLocale>
                {
                    new SpeechLocale("Chinese (Simplified)", "zh-CN", true),
                    new SpeechLocale("Chinese (Hongkong)", "zh-HK", true),
                    new SpeechLocale("Chinese (Taiwan)", "zh-TW", true)
                });

            //COGNITIVE SERVICES ONLY
            var catalan = new Language("Catalan", "ca",
                new List<SpeechLocale> {new SpeechLocale("Catalan", "ca-ES", false)}, null, false);
            var finnish = new Language("Finnish", "fi",
                new List<SpeechLocale> {new SpeechLocale("Finnish", "fi-FI", false)}, null, false);
            var japanese = new Language("Japanese", "ja",
                new List<SpeechLocale> {new SpeechLocale("Japanese", "ja-JP", true)}, null, false);
            var korean = new Language("Korean", "ko",
                new List<SpeechLocale> {new SpeechLocale("Korean", "ko-KR", false)}, null, false);

            //PONS
            var greek = new Language("Greek", "el", null);
            var latin = new Language("Latin", "la", null);
            var slovenian = new Language("Slovenian", "sl", null);
            var turkish = new Language("Turkish", "tr", null);
            var czech = new Language("Czech", "cs", null);
            var hungarian = new Language("Hungarian", "hu", null);
            var elvish = new Language("Elvish", "lb", null);

            //TRANSLATIONS
            arabic.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "aren"), new KeyValuePair<Language, string>(german, "arde")
            });
            danish.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "dade")});
            german.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(arabic, "arde"), new KeyValuePair<Language, string>(chinese, "dezh"),
                new KeyValuePair<Language, string>(dutch, "denl"), new KeyValuePair<Language, string>(english, "deen"),
                new KeyValuePair<Language, string>(french, "defr"), new KeyValuePair<Language, string>(greek, "deel"),
                new KeyValuePair<Language, string>(italian, "deit"), new KeyValuePair<Language, string>(latin, "dela"),
                new KeyValuePair<Language, string>(polish, "depl"),
                new KeyValuePair<Language, string>(portuguese, "dept"),
                new KeyValuePair<Language, string>(russian, "deru"),
                new KeyValuePair<Language, string>(slovenian, "desl"),
                new KeyValuePair<Language, string>(spanish, "dees"), new KeyValuePair<Language, string>(turkish, "detr"),
                new KeyValuePair<Language, string>(czech, "csde"), new KeyValuePair<Language, string>(danish, "dade"),
                new KeyValuePair<Language, string>(hungarian, "dehu"),
                new KeyValuePair<Language, string>(norwegian, "deno"),
                new KeyValuePair<Language, string>(swedish, "desv"), new KeyValuePair<Language, string>(elvish, "delb")
            });
            english.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(arabic, "aren"), new KeyValuePair<Language, string>(chinese, "enzh"),
                new KeyValuePair<Language, string>(french, "enfr"), new KeyValuePair<Language, string>(german, "deen"),
                new KeyValuePair<Language, string>(italian, "enit"), new KeyValuePair<Language, string>(polish, "enpl"),
                new KeyValuePair<Language, string>(portuguese, "enpt"),
                new KeyValuePair<Language, string>(russian, "enru"),
                new KeyValuePair<Language, string>(slovenian, "ensl"),
                new KeyValuePair<Language, string>(spanish, "enes")
            });
            spanish.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(chinese, "eszh"),
                new KeyValuePair<Language, string>(english, "enes"), new KeyValuePair<Language, string>(french, "esfr"),
                new KeyValuePair<Language, string>(german, "dees"), new KeyValuePair<Language, string>(polish, "espl"),
                new KeyValuePair<Language, string>(portuguese, "espt"),
                new KeyValuePair<Language, string>(slovenian, "essl")
            });
            french.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(chinese, "frzh"),
                new KeyValuePair<Language, string>(english, "enfr"), new KeyValuePair<Language, string>(german, "defr"),
                new KeyValuePair<Language, string>(polish, "frpl"),
                new KeyValuePair<Language, string>(slovenian, "frsl"),
                new KeyValuePair<Language, string>(spanish, "esfr")
            });
            italian.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "enit"), new KeyValuePair<Language, string>(german, "deit"),
                new KeyValuePair<Language, string>(polish, "itpl"),
                new KeyValuePair<Language, string>(slovenian, "itsl")
            });
            norwegian.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "deno")});
            dutch.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "denl")});
            polish.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "enpl"), new KeyValuePair<Language, string>(french, "frpl"),
                new KeyValuePair<Language, string>(german, "depl"), new KeyValuePair<Language, string>(italian, "itpl"),
                new KeyValuePair<Language, string>(russian, "plru"), new KeyValuePair<Language, string>(spanish, "espl")
            });
            portuguese.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "enpt"), new KeyValuePair<Language, string>(german, "dept"),
                new KeyValuePair<Language, string>(spanish, "espt")
            });
            russian.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "enru"), new KeyValuePair<Language, string>(german, "deru"),
                new KeyValuePair<Language, string>(polish, "plru")
            });
            swedish.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "desv")});
            chinese.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "enzh"), new KeyValuePair<Language, string>(french, "frzh"),
                new KeyValuePair<Language, string>(german, "dezh"), new KeyValuePair<Language, string>(spanish, "eszh")
            });
            greek.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "deel")});
            latin.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "dela")});
            slovenian.AddTranslationLanguage(new[]
            {
                new KeyValuePair<Language, string>(english, "ensl"), new KeyValuePair<Language, string>(french, "frsl"),
                new KeyValuePair<Language, string>(german, "desl"), new KeyValuePair<Language, string>(italian, "itsl"),
                new KeyValuePair<Language, string>(spanish, "essl")
            });
            turkish.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "detr")});
            czech.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "csde")});
            hungarian.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "dehu")});
            elvish.AddTranslationLanguage(new[] {new KeyValuePair<Language, string>(german, "delb")});

            //POPULATING
            LanguageTree.AddRange(new[]
            {
                arabic, danish, german, english, spanish, french, italian, norwegian, dutch, polish, portuguese,
                russian, swedish, chinese, greek, latin, slovenian, turkish, czech, hungarian, elvish
            });
            LanguageTree.AddRange(new[] {catalan, finnish, japanese, korean});

            //SORTING
            LanguageTree.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
        }

        /// <summary>
        /// Sets user definied language locale for specified language.
        /// </summary>
        /// <param name="languageCode">Language code in format: xx (e.g. en)</param>
        /// <param name="languageLocale">Language locale code in format: xx-XX (e.g. en-US)</param>
        /// <returns>Returns true on success</returns>
        public bool SetLanguageLocale(string languageCode, string languageLocale)
        {
            Language foundLang = LanguageTree.Find(x => x.PonsCode.Equals(languageCode));
            return foundLang != null && foundLang.SetCurrentLocale(languageLocale);
        }
    }
}