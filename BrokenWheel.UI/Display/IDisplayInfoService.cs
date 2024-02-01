namespace BrokenWheel.UI.Display
{
    /// <summary>
    /// An interface used by <see cref="DisplayInfo"/> to get information on the implementation's display.
    /// </summary>
    public interface IDisplayInfoService
    {
        /// <summary>
        /// Gets the resolution of the game's window.
        /// </summary>
        /// <returns> Item 1: width, Item 2: height. </returns>
        (int, int) WindowResolution();
        
        /// <summary>
        /// Gets the resolution of the low-res user interface.
        /// </summary>
        /// <returns> Item 1: width, Item 2: height. </returns>
        (int, int) UIResolution();
    }
}
