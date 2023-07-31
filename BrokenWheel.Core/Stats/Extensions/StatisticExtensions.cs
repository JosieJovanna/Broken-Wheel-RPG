using BrokenWheel.Core.Stats.Processing;

namespace BrokenWheel.Core.Stats.Extensions
{
    internal static class StatisticExtensions // TODO: this should be covered by methods/stat info
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
