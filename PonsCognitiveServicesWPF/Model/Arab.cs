using System.Collections.Generic;
using Newtonsoft.Json;

namespace PonsCognitiveServicesWPF.Model
{
    /// <summary>
    /// An arab contains a header (arabic numeral) and stands for a specific meaning of the
    /// headword described in the rom. For example, the "substantive"-rom of "cut" has 12 arabs.
    /// </summary>
    public class Arab
    {
        [JsonProperty("header")]
        public string Header;
        [JsonProperty("translations")]
        public List<Translation> Translations;

        public Arab()
        {
            Translations = new List<Translation>();
        }
    }
}
