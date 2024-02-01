using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public class StatUpdatedEvent : AbstractStatUpdateEvent<Statistic>
    {
        public StatUpdatedEvent(
            object sender,
            string entityId,
            StatInfo statInfo,
            Statistic stat)
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
