using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Events
{
    public abstract class AbstractStatUpdatedEvent<T> : CustomOverrideEnumGameEvent<Stat>
        where T : Statistic
    {
        public StatInfo StatInfo { get; }
        public T Statistic { get; }
        
        protected AbstractStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, T statUpdateData) 
            : base(sender, entityId, statInfo?.Stat)
        {
            Statistic = statUpdateData;
            StatInfo = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
            IsOverridden = statInfo.IsCustom;
            if (IsOverridden)
                OverrideCode = statInfo.Code;
        }
    }
}
