using System.Collections.Generic;
using Newtonsoft.Json;

namespace PonsCognitiveServicesWPF.Model
{
    /// <summary>
    /// A rom contains a headword and linguistic data related to this headword. The headword is
    /// usually the word you would lookup in a printed dictionary.
    /// headword_full may include additional information, such as phonetics, gender, etc. .
    /// For each part of speech there is one rom (roman numeral). For example "cut" may be a
    /// noun, adjective, interjection, transitive or intransitive verb and has the roms I to V.
    /// </summary>
    public class Rom
    {
        [JsonProperty("headword")]
        public string Headword;
        [JsonProperty("headword_full")]
        public string HeadwordFull;
        [JsonProperty("wordclass")]
        public string Wordclass;
        [JsonProperty("arabs")]
        public List<Arab> Arabs;

        public Rom()
        {
            Arabs = new List<Arab>();
        }
    }
}
