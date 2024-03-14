using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public class StatUpdatedEvent : AbstractStatUpdateEvent<Statistic>
    {
        public StatUpdatedEvent(
            StatInfo statInfo,
            Statistic stat)
            : base(statInfo, stat)
        {
        }
    }
}
