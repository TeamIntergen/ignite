using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class AccessDto
    {
        [JsonProperty("SessionCatalog")]
        public bool SessionCatalog { get; set; }
        [JsonProperty("PlaylistBuilder")]
        public bool PlaylistBuilder { get; set; }
        [JsonProperty("MyIgnite")]
        public bool MyIgnite { get; set; }
        [JsonProperty("ScheduleBuilder")]
        public bool ScheduleBuilder { get; set; }
        [JsonProperty("ExpertConfig")]
        public bool ExpertConfig { get; set; }
        [JsonProperty("MobileApp")]
        public bool MobileApp { get; set; }
        [JsonProperty("MeetingScheduler")]
        public bool MeetingScheduler { get; set; }
        [JsonProperty("LocationBasedMessaging")]
        public bool LocationBasedMessaging { get; set; }
    }
}