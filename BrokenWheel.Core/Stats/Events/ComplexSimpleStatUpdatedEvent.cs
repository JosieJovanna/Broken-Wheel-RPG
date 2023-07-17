using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Events
{
    public class ComplexSimpleStatUpdatedEvent : AbstractStatUpdatedEvent<ComplexStatUpdate>
    {
        public ComplexSimpleStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, ComplexStatUpdate stat)
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
