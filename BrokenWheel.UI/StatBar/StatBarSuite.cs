using System.Collections.Generic;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.StatBar
{
    public class StatBarSuite : IStatBarSuite
    {
        public void StatUpdateHandler(object sender, StatUpdateEventArgs args)
        {
            throw new System.NotImplementedException();
        }

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
