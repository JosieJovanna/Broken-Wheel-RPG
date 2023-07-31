namespace BrokenWheel.Core.Stats.Events
{
    public class ComplexStatUpdatedEvent : AbstractStatUpdatedEvent<ComplexStat>
    {
        public ComplexStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, ComplexStat stat)
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
