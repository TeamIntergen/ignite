using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class RulesDto
    {
        [JsonProperty("UserAccess")]
        public List<ProfileDto> UserAccess { get; set;  }
    }
}