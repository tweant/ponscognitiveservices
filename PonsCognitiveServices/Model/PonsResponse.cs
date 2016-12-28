using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PonsCognitiveServices.Helpers;

namespace PonsCognitiveServices.Model
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
