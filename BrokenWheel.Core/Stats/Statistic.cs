namespace BrokenWheel.Core.Stats
{
    public class Statistic
    {
        /// <summary>
        /// The current value of the stat. Will not be higher than <see cref="Maximum"/>, if applicable.
        /// </summary>
        public int Value { get; internal set; }

        /// <summary>
        /// The amount added to or subtracted from the stat.
        /// </summary>
        public int Modifier { get; internal set; }

        /// <summary>
        /// The final number needed for most stat checks. Will not be higher than <see cref="Maximum"/>, if applicable.
        /// For simple stats, value plus modifier; for complex, just the value, no higher than effective maximum.
        /// </summary>
        public int EffectiveValue { get; internal set; }

        /// <summary>
        /// The maximum value this stat can have.
        /// </summary>
        public int Maximum { get; internal set; }
    }
}
