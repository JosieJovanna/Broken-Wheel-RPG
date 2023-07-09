namespace BrokenWheel.Core.Stats.Processing
{
    public interface IStatBox
    {
        void SaveStats();
        void LoadStats();
        
        T GetStat<T>(StatType stat) where T : IStatistic;
        T GetStat<T>(string stat) where T : IStatistic;

        IStatistic GetSimpleStat(StatType stat);
        IStatistic GetSimpleStat(string stat);

        IComplexStatistic GetComplexStat(StatType stat);
        IComplexStatistic GetComplexStat(string stat);
    }
}
