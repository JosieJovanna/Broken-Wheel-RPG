namespace BrokenWheel.Core.Stats.Extensions
{
    public static class StatisticExtensions
    {
        public static bool IsComplex(this IStatistic statistic)
        {
            return statistic.GetType().IsAssignableFrom(typeof(IComplexStatistic));
        }

        public static bool TryAsComplex(this IStatistic statistic, out IComplexStatistic complexStatistic)
        {
            var isComplex = statistic.IsComplex();
            complexStatistic = isComplex ? statistic.AsComplex() : null;
            return isComplex;
        }

        public static IComplexStatistic AsComplex(this IStatistic statistic)
        {
            return (IComplexStatistic)statistic;
        }
    }
}
