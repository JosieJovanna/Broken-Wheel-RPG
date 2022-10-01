using LorendisCore.Common.Stats.Enum;

namespace LorendisCore.Common.Stats.Processing
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

        public SimpleStat GetSimpleStat(Stat stat) => GetStat<SimpleStat>(stat);
        public SimpleStat GetSimpleStat(string stat) => GetStat<SimpleStat>(stat);

        public ComplexStat GetComplexStat(Stat stat) => GetStat<ComplexStat>(stat);
        public ComplexStat GetComplexStat(string stat) => GetStat<ComplexStat>(stat);
        
        public T GetStat<T>(Stat stat) where T : IStatObject => GetStat<T>(stat.GetName());
        public T GetStat<T>(string stat) where T : IStatObject
        {
            throw new System.NotImplementedException();
            
            // get the save file for the given thing
            // get the stat by name
            // JSON is tempting but inefficient; I think I'll need a string followed by ints in a row
            // (and so I'll probably have to specify format by type)
        }
    }
}