using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Events
{
    public class SimpleStatUpdatedEvent : AbstractStatUpdatedEvent<Stat>
    {
        public SimpleStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, Stat stat) 
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
