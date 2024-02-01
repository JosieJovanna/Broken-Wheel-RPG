using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Processing
{
    /// <summary>
    /// A statistic tracker with a <see cref="Stat"/> type, value, and modifier.
    /// The <see cref="Value"/> is more stable, being for example skill, which only changes during level-ups.
    /// The <see cref="Modifier"/> is more erratic, being set by spells, equipment, et cetera.
    /// </summary>
    internal interface IStatisticProcessor
    {
        /// <summary>
        /// The type of <see cref="Stat"/> this object represents.
        /// </summary>
        StatInfo StatInfo { get; }
        
        /// <summary>
        /// The value of the stat. Cannot be negative; attempting to set a negative value will in fact set it to zero.
        /// </summary>
        int Value { get;  }

        /// <summary>
        /// The modifier of the stat. Can be negative; though if the total is negative, it will be returned as zero.
        /// </summary>
        int Modifier { get; set; }

        /// <summary>
        /// The sum of all numbers into the value which matters most for checks. Will not be negative.
        /// </summary>
        int EffectiveValue { get; }
    }
}
