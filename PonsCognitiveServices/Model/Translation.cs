using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PonsCognitiveServices.Model
{
    /// <summary>
    /// A translation contains a source/target-pair (the actual translations).
    /// </summary>
    public class Translation
    {
        [JsonProperty("source")]
        public string Source;
        [JsonProperty("target")]
        public string Target;
    }
}
