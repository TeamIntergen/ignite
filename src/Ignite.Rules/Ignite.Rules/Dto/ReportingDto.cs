using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class ReportingDto
    {
        [JsonProperty("SessionCounts")]
        public bool SessionCounts { get; set; }
    }
}