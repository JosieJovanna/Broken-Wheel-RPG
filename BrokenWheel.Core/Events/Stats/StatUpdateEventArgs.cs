using System;
using System.Collections.Generic;
using BrokenWheel.Core.Damage;
using BrokenWheel.Core.Damage.Dps;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Events.Stats
{
    public class StatUpdateEventArgs : GameEventArgs
    {
        public StatUpdateEventArgs(string entityGuid, IStatistic statistic)
            : base(entityGuid)
        {
            Stat = statistic ?? throw new ArgumentNullException(nameof(statistic));
        }
        
        public IStatistic Stat { get; }
        
        public StatType StatType { get => Stat.Info.Type; }

        public bool IsComplexStat() => Stat.IsComplex();

        /// <returns> Statistic as a complex statistic. If not a complex statistic, null. </returns>
        public IComplexStatistic StatAsComplex() => Stat.TryAsComplex(out var complex) ? complex : null;
    }
}
