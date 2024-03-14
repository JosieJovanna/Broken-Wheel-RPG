using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public class ComplexStatUpdatedEvent : AbstractStatUpdateEvent<ComplexStatistic>
    {
        public ComplexStatUpdatedEvent(
            StatInfo statInfo,
            ComplexStatistic stat)
            : base(statInfo, stat)
        {
        }
    }
}
