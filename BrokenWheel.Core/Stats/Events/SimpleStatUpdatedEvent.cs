using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Events
{
    public class SimpleStatUpdatedEvent : AbstractStatUpdatedEvent<StatUpdate>
    {
        public SimpleStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, StatUpdate stat) 
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
