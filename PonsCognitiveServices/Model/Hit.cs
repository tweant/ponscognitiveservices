using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PonsCognitiveServices.Model
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
