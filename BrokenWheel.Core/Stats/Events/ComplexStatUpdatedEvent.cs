using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Events
{
    public class ComplexStatUpdatedEvent : AbstractStatUpdatedEvent<ComplexStatUpdate>
    {
        public ComplexStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, ComplexStatUpdate stat)
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
