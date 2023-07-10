using System;
using System.Collections.Generic;
using BrokenWheel.Core.Damage;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Events.Stats
{
    public class StatUpdateEventArgs : GameEventArgs
    {
        public StatUpdateEventArgs(string entityGuid, IStatistic statistic, IList<DamageTicker> damageSources)
            : base(entityGuid)
        {
            Stat = statistic ?? throw new ArgumentNullException(nameof(statistic));
            DamageSources = damageSources ?? new List<DamageTicker>();
        }
        
        public IStatistic Stat { get; }
        
        public IList<DamageTicker> DamageSources { get; }

        public StatType StatType { get => Stat.Info.Type; }

        public bool IsComplexStat() => Stat.IsComplex();

        /// <returns> Statistic as a complex statistic. If not a complex statistic, null. </returns>
        public IComplexStatistic StatAsComplex() => Stat.TryAsComplex(out var complex) ? complex : null;
    }
}
