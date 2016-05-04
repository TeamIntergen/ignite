using System.Collections.Generic;
using System.Threading.Tasks;
using Ignite.Rules.Dto;

namespace Ignite.Rules
{
    public interface ISecurityRepository
    {
        Task<RulesDto> LoadAccessLevel();
        Task<IEnumerable<SessionsetMapDto>> LoadSessionMap();
    }
}