namespace BrokenWheel.Core.Stats.Processing
{
    public interface IStatBox
    {
        void SaveStats();
        void LoadStats();
        
        T GetStat<T>(Stat stat) where T : IStatistic;
        T GetStat<T>(string stat) where T : IStatistic;

        IStatistic GetSimpleStat(Stat stat);
        IStatistic GetSimpleStat(string stat);

        IComplexStatistic GetComplexStat(Stat stat);
        IComplexStatistic GetComplexStat(string stat);
    }
}
