using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PonsCognitiveServicesWPF.Helpers;

namespace PonsCognitiveServicesWPF.Model
{
    public class PonsResponse
    {
        [JsonProperty("lang")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PonsLanguage Language;
        [JsonProperty("hits")]
        public List<Hit> Hits;

        public PonsResponse()
        {
            Hits = new List<Hit>();
        }

    }
}
