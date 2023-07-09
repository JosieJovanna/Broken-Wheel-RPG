using System.Collections.Generic;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.StatBar
{
    public class StatBarSuite : IStatBarSuite
    {
        private class StatBarRelationship
        {
            public IComplexStatistic Stat { get; }
            public IStatBar Bar { get; }
            public int Order { get; }

            public StatBarRelationship(IComplexStatistic statistic, IStatBar statBar, int order = 99)
            {
                Stat = statistic;
                Order = order;
            }
        }

        private readonly IList<StatBarRelationship> _statBars;

        public StatBarSuite(IComplexStatistic health, IComplexStatistic stamina, IComplexStatistic willpower, 
            IList<IComplexStatistic> otherStats = null)
        {
        }
        
        public void StatUpdateHandler(object sender, StatUpdateEventArgs args) => Update();

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public IList<IComplexStatistic> GetStats()
        {
            throw new System.NotImplementedException();
        }

        public void AddStat(IComplexStatistic statistic)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveStat(StatType statType)
        {
            throw new System.NotImplementedException();
        }
    }
}
