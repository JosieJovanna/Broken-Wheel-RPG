using System;
using System.Collections.Generic;
using BrokenWheel.Core.Damage;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Events
{
    public class StatUpdateEventArgs : EventArgs
    {
        public StatUpdateEventArgs(IStatistic statistic, IList<DamageTicker> damageSources)
        {
            Statistic = statistic ?? throw new ArgumentNullException(nameof(statistic));
            DamageSources = damageSources ?? new List<DamageTicker>();
        }
        
        public IStatistic Statistic { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public IList<DamageTicker> DamageSources { get; }

        /// <summary>
        /// The type of the 
        /// </summary>
        public StatType StatType { get => Statistic.Type.StatType; }

        /// <returns> Whether the statistic is complex. </returns>
        public bool IsComplexStat() => Statistic.IsComplex();

        /// <returns> Statistic as a complex statistic. If not a complex statistic, null. </returns>
        public IComplexStatistic StatisticAsComplex() => Statistic.TryAsComplex(out var complex) ? complex : null;
    }
}
