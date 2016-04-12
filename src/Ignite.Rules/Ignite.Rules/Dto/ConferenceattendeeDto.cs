using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class ConferenceattendeeDto
    {
        [JsonProperty("AttendeeExternalCustomerAndPartner")]
        public ProfileDto AttendeeExternalCustomerAndPartner { get; set; }
        [JsonProperty("AttendeeSponsor")]
        public ProfileDto AttendeeSponsor { get; set; }
        [JsonProperty("AttendeeExhibitor")]
        public ProfileDto AttendeeExhibitor { get; set; }
        [JsonProperty("AttendeeMicrosoft")]
        public ProfileDto AttendeeMicrosoft { get; set; }
        [JsonProperty("AttendeeStudent")]
        public ProfileDto AttendeeStudent { get; set; }
        [JsonProperty("AttendeeFaculty")]
        public ProfileDto AttendeeFaculty { get; set; }
        [JsonProperty("AttendeeVIP")]
        public ProfileDto AttendeeVIP { get; set; }
        [JsonProperty("AttendeeVIPMicrosoft")]
        public ProfileDto AttendeeVIPMicrosoft { get; set; }
    }
}