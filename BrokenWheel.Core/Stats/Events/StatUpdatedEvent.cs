using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public class StatUpdatedEvent : AbstractStatUpdateEvent<Statistic>
    {
        public StatUpdatedEvent(
            object sender,
            StatInfo statInfo,
            Statistic stat)
            : base(sender, statInfo, stat)
        {
        }
    }
}
