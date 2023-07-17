namespace BrokenWheel.Core.Stats.Events
{
    public class StatUpdate
    {
        /// <summary>
        /// The current value of the stat.
        /// </summary>
        public int Value { get; internal set; }
        
        /// <summary>
        /// The amount added to or subtracted from the stat.
        /// </summary>
        public int Modifier { get; internal set; }
        
        /// <summary>
        /// The final number needed for most stat checks.
        /// For simple stats, value plus modifier; for complex, just the value, no higher than effective maximum.
        /// </summary>
        public int EffectiveValue { get; internal set; }
    }
}
