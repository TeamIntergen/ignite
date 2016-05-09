using System;
using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SessionsetaccessDto
    {
        private DateTimeOffset _applicableFrom;
        private DateTimeOffset _applicableTo;

        [JsonProperty("Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("ApplicableFrom")]
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