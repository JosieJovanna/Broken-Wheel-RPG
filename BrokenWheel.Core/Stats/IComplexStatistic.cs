namespace BrokenWheel.Core.Stats
{
    /// /// <summary>
    /// A simple implementation of <see cref="IComplexStatistic"/>.
    /// Has a value, which ranges between zero and the <see cref="EffectiveMaximum"/>. This would be the current value.
    /// Has a maximum, which is what often changes with level, in the case of HP, WP, et cetera. More stable; non-negative.
    /// Has a modifier to the <see cref="EffectiveMaximum"/>, positive or negative. Set by effects, equipment, et cetera.
    /// Has exhaustion, which is a gradual drain on the <see cref="Stat"/> that happens with time and use.
    /// </summary>
    public interface IComplexStatistic : IStatistic
    {
        /// <summary>
        /// The value of the <see cref="Stat"/>.
        /// Will be equal to or lower than the <see cref="EffectiveMaximum"/>, and non-negative.
        /// </summary>
        new int Value { get; set; }
        
        /// <summary>
        /// The modifier of the <see cref="EffectiveMaximum"/> value the stat can have.
        /// </summary>
        new int Modifier { get; set; }

        /// <summary>
        /// The value of the <see cref="Stat"/>, which has no modifiers. Equal to <see cref="Value"/>;
        /// will be greater than or equal to zero, and less than or equal to <see cref="EffectiveMaximum"/>.
        /// </summary>
        new int EffectiveValue { get; }

        /// <summary>
        /// The maximum <see cref="Value"/> that the stat can have,
        /// before modification by <see cref="Modifier"/> and <see cref="Exhaustion"/>.
        /// </summary>
        int Maximum { get; set; }

        /// <summary>
        /// A penalty the the <see cref="EffectiveMaximum"/> a stat can have, which often increases with time.
        /// Cannot be negative; attempting to set a negative value will in fact set it to zero.
        /// </summary>
        int Exhaustion { get; set; }
        
        /// <summary>
        /// The total amount which the <see cref="Value"/> can reach,
        /// as calculated by '<see cref="Maximum"/> + <see cref="Modifier"/> - <see cref="Exhaustion"/>'
        /// </summary>
        int EffectiveMaximum { get; }
    }
}
