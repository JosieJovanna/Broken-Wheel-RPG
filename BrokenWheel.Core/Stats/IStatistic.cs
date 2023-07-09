using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats
{
    /// <summary>
    /// A statistic tracker with a <see cref="StatType"/> type, value, and modifier.
    /// The <see cref="Value"/> is more stable, being for example skill, which only changes during level-ups.
    /// The <see cref="Modifier"/> is more erratic, being set by spells, equipment, et cetera.
    /// </summary>
    public interface IStatistic
    {
        /// <summary>
        /// The type of <see cref="StatType"/> this object represents.
        /// </summary>
        StatTypeInfo Type { get; }
        
        /// <summary>
        /// The value of the stat. Cannot be negative; attempting to set a negative value will in fact set it to zero.
        /// </summary>
        int Value { get; set; }

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
