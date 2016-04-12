using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SpeakerDto
    {
        [JsonProperty("Speaker External Customer & Partner- HOTEL")]
        public ProfileDto SpeakerExternalCustomerPartnerHOTEL { get; set; }

        [JsonProperty("Speaker External Customer & Partner- NO HOTEL")]
        public ProfileDto SpeakerExternalCustomerPartnerNOHOTEL { get; set; }

        [JsonProperty("Speaker and Expert Microsoft - NO HOTEL")]
        public ProfileDto SpeakerandExpertMicrosoftNOHOTEL { get; set; }

        [JsonProperty("Speaker Microsoft- NO HOTEL")]
        public ProfileDto SpeakerMicrosoftNOHOTEL { get; set; }
    }
}