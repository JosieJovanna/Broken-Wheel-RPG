using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public abstract class AbstractStatUpdateEvent<T> : StringEnumSwitchGameEvent<Stat>
        where T : Statistic
    {
        public StatInfo StatInfo { get; }
        public T Statistic { get; }

        protected AbstractStatUpdateEvent(object sender, string entityId, StatInfo statInfo, T statUpdateData)
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
