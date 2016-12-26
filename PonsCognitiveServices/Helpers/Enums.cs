namespace PonsCognitiveServices.Helpers
{
    public enum Language
    {
        Polish,
        English,
        Deutsch
    }

    public sealed class PonsLanguage
    {
        private readonly string _shortName;
        private readonly int _value;

        public static readonly PonsLanguage Deutsch = new PonsLanguage(1, "de");
        public static readonly PonsLanguage ModernGreek = new PonsLanguage(2, "el");
        public static readonly PonsLanguage English = new PonsLanguage(3, "en");
        public static readonly PonsLanguage Spanish = new PonsLanguage(4, "es");
        public static readonly PonsLanguage French = new PonsLanguage(5, "fr");
        public static readonly PonsLanguage Italian = new PonsLanguage(6, "it");
        public static readonly PonsLanguage Polish = new PonsLanguage(7, "pl");
        public static readonly PonsLanguage Portuguese = new PonsLanguage(8, "pt");
        public static readonly PonsLanguage Russian = new PonsLanguage(9, "ru");
        public static readonly PonsLanguage Slovene = new PonsLanguage(10, "sl");
        public static readonly PonsLanguage Turkish = new PonsLanguage(11, "tr");
        public static readonly PonsLanguage StandardChinese = new PonsLanguage(12, "zh");

        private PonsLanguage(int value, string shortName)
        {
            _shortName = shortName;
            _value = value;
        }

        public override string ToString()
        {
            return _shortName;
        }
    }
}
