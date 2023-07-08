using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Processing
{
    public class StatBox : IStatBox
    {
        // TODO: constructor to specify entity to search for in memory
        
        public void SaveStats()
        {
            throw new System.NotImplementedException();
        }

        public void LoadStats()
        {
            throw new System.NotImplementedException();
        }

        public IStatistic GetSimpleStat(Stat stat) => GetStat<IStatistic>(stat);
        public IStatistic GetSimpleStat(string stat) => GetStat<IStatistic>(stat);

        public IComplexStatistic GetComplexStat(Stat stat) => GetStat<IComplexStatistic>(stat);
        public IComplexStatistic GetComplexStat(string stat) => GetStat<IComplexStatistic>(stat);
        
        public T GetStat<T>(Stat stat) where T : IStatistic => GetStat<T>(stat.GetName());
        public T GetStat<T>(string stat) where T : IStatistic
        {
            throw new System.NotImplementedException();
            
            // get the save file for the given thing
            // get the stat by name
            // JSON is tempting but inefficient; I think I'll need a string followed by ints in a row
            // (and so I'll probably have to specify format by type)
        }
    }
}
