using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ignite.Rules.Dto;
using Newtonsoft.Json;

namespace Ignite.Rules
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly ILoader _loader;
        private readonly ISettings _settings;

        public SecurityRepository(ILoader loader, ISettings settings)
        {
            _loader = loader;
            _settings = settings;
        }

        public async Task<RulesDto> LoadAccessLevel()
        {
            try
            {
                var ruleContent = await _loader.Load(_settings.RulesPath);
                var rules = JsonConvert.DeserializeObject<RulesDto>(ruleContent);
                if (rules?.UserAccess != null)
                {
                    return rules;
                }

                throw new InvalidOperationException("Unable to find user access level.");

            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to load access level", e);
            }
        }

        public async Task<IEnumerable<SessionsetMapDto>> LoadSessionMap()
        {
            try
            {
                var sessionMapContent = await _loader.Load(_settings.SessionMapPath);
                var map = JsonConvert.DeserializeObject<SessionSetMapListDto>(sessionMapContent);
                return map.SessionSetMap;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to load session map", e);
            }
        }
    }
}