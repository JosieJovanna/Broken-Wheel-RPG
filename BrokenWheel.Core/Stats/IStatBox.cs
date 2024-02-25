using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats
{
    /// <summary>
    /// The object responsible for keeping track of a given being's statistics, to be queried.
    /// </summary>
    public interface IStatBox
    {
        float BaseWalkSpeed { get; }
        float BaseCrouchSpeed { get; }
        float BaseCrawlSpeed { get; }
        float BaseSprintSpeed { get; }
        // TODO: sprinting acceleration?

        Statistic GetStatistic(Stat statEnum);
        Statistic GetStatByCode(string statCode);
        Statistic GetStatByName(string statName, string statNamespace = DebugConstants.BROKEN_WHEEL_NAMESPACE);

        ComplexStatistic GetComplexStatistic(Stat statEnum);
        ComplexStatistic GetComplexStatByCode(string statCode);
        ComplexStatistic GetComplexStatByName(string statName, string statNamespace = DebugConstants.BROKEN_WHEEL_NAMESPACE);
    }
}
