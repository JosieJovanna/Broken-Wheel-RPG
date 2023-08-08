using BrokenWheel.Core.Stats.Processing;

namespace BrokenWheel.Core.Stats.Extensions
{
    internal static class StatisticExtensions // TODO: this should be covered by methods/stat info
    {
        public static bool IsComplex(this IStatisticProcessor statisticProcessor)
        {
            return statisticProcessor.GetType().IsAssignableFrom(typeof(IComplexStatisticProcessor));
        }

        public static bool TryAsComplex(this IStatisticProcessor statisticProcessor, out IComplexStatisticProcessor complexStatisticProcessor)
        {
            var isComplex = statisticProcessor.IsComplex();
            complexStatisticProcessor = isComplex ? statisticProcessor.AsComplex() : null;
            return isComplex;
        }

        public static IComplexStatisticProcessor AsComplex(this IStatisticProcessor statisticProcessor)
        {
            return (IComplexStatisticProcessor)statisticProcessor;
        }
    }
}
