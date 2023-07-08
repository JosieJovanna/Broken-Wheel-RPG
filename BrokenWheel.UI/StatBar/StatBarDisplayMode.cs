namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// The way that the <see cref="IStatBar"/> displays.
    /// </summary>
    public enum StatBarDisplayMode
    {
        /// <summary>
        /// All stat bars share the same maximum width, with values scaled to fit.
        /// </summary>
        FixedWidth = default,
        
        /// <summary>
        /// All stat bars share the same maximum width before modification.
        /// Modifiers extend or shorten bars.
        /// </summary>
        FixedMaxScalingMod = 1,
        
        /// <summary>
        /// Stat bars have a set point-to-pixel ratio.
        /// When stat bar reaches maximum length, that bar scales to max width.
        /// </summary>
        ScaleByPointsIndividualMaxScaling = 2,
        
        /// <summary>
        /// Stat bars have a set point-to-pixel ratio.
        /// when stat bar reaches maximum length, it stays at maximum, and all other bars share that new scale.
        /// </summary>
        ScaleByPointsAllScaleToMax = 3
    }
}
