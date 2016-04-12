using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class RulesDto
    {
        [JsonProperty("UserAccess")]
        public UseraccessDto[] UserAccess { get; set;  }
    }
}