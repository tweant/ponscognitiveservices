using System.Collections.Generic;
using Newtonsoft.Json;

namespace PonsCognitiveServicesWPF.Model
{
    public class Hit
    {
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("opendict")]
        public bool IsOpenDict;
        [JsonProperty("roms")]
        public List<Rom> Roms;

        public Hit()
        {
            Roms = new List<Rom>();
        }
    }
}
