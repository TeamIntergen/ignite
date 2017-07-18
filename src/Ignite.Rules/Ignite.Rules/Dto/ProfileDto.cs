using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class ProfileDto
    {
        [JsonProperty("Identifiers")]
        public string[] Identifiers { get; set; } = {};
        public string Identifier { get; set; }
        [JsonProperty("Access")]
        public AccessDto Access { get; set; }
        [JsonProperty("Reporting")]
        public ReportingDto Reporting { get; set; }
        [JsonProperty("EvalAccess")]
        public EvalaccessDto[] EvalAccess { get; set; }
        [JsonProperty("SessionSetAccess")]
        public SessionsetaccessDto[] SessionSetAccess { get; set; }
        [JsonProperty("VisibleAttendeeTypes")]
        public string[] VisibleAttendeeTypes { get; set; }
    }
}