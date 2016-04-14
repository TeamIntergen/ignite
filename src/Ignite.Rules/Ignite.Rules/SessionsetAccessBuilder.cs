using Ignite.Rules.Dto;

namespace Ignite.Rules
{
    public class SessionsetAccessBuilder
    {
        public SessionsetAccess Build(SessionsetaccessDto dto)
        {
            return new SessionsetAccess(dto.SchedulerStandard, dto.Lab, dto.SchedulerTLF, dto.SchedulerPress);
        }

    }
}