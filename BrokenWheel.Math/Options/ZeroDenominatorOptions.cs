namespace BrokenWheel.Math.Options
{
    /// <summary>
    /// Options for how the fraction behaves when a zero denominator is set.
    /// </summary>
    public enum ZeroDenominatorOption
    {
        /// <summary>
        /// When attempting to set the denominator to zero, will throw a <see cref="FractionException"/>.
        /// </summary>
        ThrowOnSetting = 0,
        /// <summary>
        /// When evaluating the fraction to a single value, will throw a <see cref="FractionException"/>.
        /// </summary>
        ThrowOnGetting = 1,
        /// <summary>
        /// When setting the denominator to zero, the numerator will be set to zero, and the denominator to one.
        /// </summary>
        SetValueToZero = 2,
        /// <summary>
        /// When getting the fraction as a single value, returns zero, but does not alter the true values.
        /// </summary>
        GetValueAsZero = 3
    }
}
