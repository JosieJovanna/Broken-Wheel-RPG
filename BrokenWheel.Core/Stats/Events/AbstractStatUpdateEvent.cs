using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public abstract class AbstractStatUpdateEvent<T> : GameEvent
        where T : Statistic
    {
        public StatInfo StatInfo { get; }
        public T Statistic { get; }

        protected AbstractStatUpdateEvent(object sender, StatInfo statInfo, T statUpdateData)
            : base(sender, statInfo.Id())
        {
            Statistic = statUpdateData;
            StatInfo = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
        }
    }
}
