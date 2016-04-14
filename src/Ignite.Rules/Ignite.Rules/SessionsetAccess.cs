namespace Ignite.Rules
{
    public class SessionsetAccess
    {
        public SessionsetAccess(bool schedulerStandard, bool lab, bool schedulerTlf, bool schedulerPress)
        {
            SchedulerStandard = schedulerStandard;
            Lab = lab;
            SchedulerTLF = schedulerTlf;
            SchedulerPress = schedulerPress;
        }

        public bool SchedulerStandard { get; private set; }
        public bool Lab { get; private set; }
        public bool SchedulerTLF { get; private set; }
        public bool SchedulerPress { get; private set; }
    }
}