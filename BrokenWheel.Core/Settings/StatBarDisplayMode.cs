namespace BrokenWheel.Core.Settings
{
    /// <summary>
    /// The way that the <see cref="IStatBar"/> displays.
    /// </summary>
    public enum StatBarDisplayMode
    {
        /// <summary>
        /// All stat bars share the same length, no matter what.
        /// </summary>
        FixedLength = default,
        
        /// <summary>
        /// All stat bars share the same maximum width based on their maximum value.
        /// The stat modifier then extends or shrinks that bar with the same proportion of points per pixel.
        /// </summary>
        FixedLengthBeforeMod = 1,
        
        /// <summary>
        /// All stat bars have a same point-to-pixel ratio. When one stat bar reaches maximum length,
        /// it will not extend past; rather, its ratio becomes the ratio used by all other bars.
        /// </summary>
        UniformPointsPerPixel = 2,
        
        /// <summary>
        /// All stat bars have a set point-to-pixel ratio. When a stat bar reaches the maximum length,
        /// it will not grow any longer, and its ratio will no longer be uniform to the others.
        /// </summary>
        UniformPointsPerPixelUntilMaxLength = 3
    }
}
