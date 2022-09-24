namespace LorendisCore.Common.Stats
{
    public interface IStatBox
    {
        void SaveStats();
        void LoadStats();
        
        T GetStat<T>(Stat stat) where T : IStatObject;
        T GetStat<T>(string stat) where T : IStatObject;

        SimpleStat GetSimpleStat(Stat stat);
        SimpleStat GetSimpleStat(string stat);

        ComplexStat GetComplexStat(Stat stat);
        ComplexStat GetComplexStat(string stat);
    }
}