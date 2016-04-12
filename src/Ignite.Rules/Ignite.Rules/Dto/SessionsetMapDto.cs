using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SessionsetMapDto
    {
        [JsonProperty("HubbId")]
        public int HubbId { get; set; }
        [JsonProperty("SessionTypeName")]
        public string SessionTypeName { get; set; }
        [JsonProperty("SessionSets")]
        public IEnumerable<string> SessionSets { get; set; }
    }
}