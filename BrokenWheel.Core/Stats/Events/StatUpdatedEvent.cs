using System;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Core.Stats.Events
{
    public class StatUpdatedEvent : CustomOverrideEnumGameEvent<StatType>
    {
        public StatInfo StatInfo { get; }
        public Stat Stat { get; }
        
        public StatUpdatedEvent(object sender, string entityId, StatInfo statInfo, Stat stat)
            : base(sender, entityId, statInfo.Type, statInfo.IsCustom ? statInfo.Code : null)
        { // TODO: replace the typing on the event with an event emitter, which can be generic, and handles sending and registering these events.
            Stat = stat;
            StatInfo = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
            IsOverridden = statInfo.IsCustom;
            if (IsOverridden)
                OverrideCode = statInfo.Code;
        }
    }
}
