using Newtonsoft.Json;

namespace PonsCognitiveServicesWPF.Model
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
