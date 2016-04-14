using System;
using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SessionsetaccessDto
    {
        private DateTimeOffset _applicableFrom;
        private DateTimeOffset _applicableTo;

        [JsonProperty("Scheduler-Standard")]
        public bool SchedulerStandard { get; set; }

        [JsonProperty("Lab")]
        public bool Lab { get; set; }

        [JsonProperty("Scheduler-TLF")]
        public bool SchedulerTLF { get; set; }

        [JsonProperty("Scheduler-Press")]
        public bool SchedulerPress { get; set; }

        [JsonProperty("Applicable-From")]
        public DateTimeOffset? ApplicableFrom {
            get { return _applicableFrom; }
            set { _applicableFrom = value ?? DateTimeOffset.MinValue; }
        }

        [JsonProperty("ApplicableTo")]
        public DateTimeOffset? ApplicableTo
        {
            get { return _applicableTo; }
            set { _applicableTo = value ?? DateTimeOffset.MaxValue; }
        }
    }
}