namespace BrokenWheel.Core.Time
{
    /// <summary>
    /// The most protected version of <see cref="ITimeService"/>, which should only be called from the root of the program.
    /// This service should not be used by any RPG-level code.
    /// </summary>
    public interface ITimeTicker
    {
        /// <summary>
        /// Sends appropriately-scaled delta time to all registered time functions.
        /// </summary>
        void Tick(double delta);
    }
}
