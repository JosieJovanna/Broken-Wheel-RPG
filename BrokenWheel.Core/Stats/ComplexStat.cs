namespace BrokenWheel.Core.Stats
{
    public class ComplexStat : Stat
    {
        // Inherits Value, Modifier
        
        /// <summary>
        /// The value this stat is set to reach over time.
        /// </summary>
        public int DestinationValue { get; internal set; }
        
        /// <summary>
        /// How many points of a stat are currently unusable.
        /// </summary>
        public int Exhaustion { get; internal set; }
        
        /// <summary>
        /// The maximum value that a stat can reach, after modifier, and exhaustion.
        /// </summary>
        public int EffectiveMaximum { get; internal set; }
    }
}
