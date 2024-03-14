using System;
using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public abstract class AbstractStatUpdateEvent<T>
        where T : Statistic
    {
        public StatInfo StatInfo { get; }
        public T Statistic { get; }

        protected AbstractStatUpdateEvent(StatInfo statInfo, T statUpdateData)
        {
            Statistic = statUpdateData;
            StatInfo = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
        }
    }
}
