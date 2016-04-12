using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SessionsetaccessDto
    {
        [JsonProperty("SchedulerStandard")]
        public bool SchedulerStandard { get; set; }

        [JsonProperty("Lab")]
        public bool Lab { get; set; }

        [JsonProperty("SchedulerTLF")]
        public bool SchedulerTLF { get; set; }

        [JsonProperty("SchedulerPress")]
        public bool SchedulerPress { get; set; }
    }
}