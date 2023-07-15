using System;
using BrokenWheel.Core.Exceptions;

namespace BrokenWheel.UI.Display
{
    /// <summary>
    /// A static service used in the UI slice to scale properly based off of resolutions.
    /// </summary>
    public static class DisplayInfo
    {
        private const string ERR_NOT_INITIALIZED = nameof(DisplayInfo) + " has not been properly initialized.";
        
        /// <summary>
        /// The service in charge of actually getting the information from the real implementation of this system.
        /// </summary>
        private static IDisplayInfoService _display;

        /// <summary>
        /// To be called when starting the game. Initializes a static provider for use in the RPG system's UI.
        /// </summary>
        /// <param name="displayInfoService"> A service used to get information from the game implementation's display. </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Initialize(IDisplayInfoService displayInfoService)
        {
            _display = displayInfoService ?? throw new ArgumentNullException(nameof(displayInfoService));
        }

        /// <summary>
        /// Checks if the service has been initialized, and throws an exception if not.
        /// </summary>
        /// <exception cref="StaticServiceNotInitializedException"> If not initialized. </exception>
        public static void ValidateIsInitialized()
        {
            if (_display == null)
                throw new StaticServiceNotInitializedException(nameof(DisplayInfo));
        }
        
        /// <summary>
        /// Gets the real pixel resolution of the display.
        /// </summary>
        /// <returns> Item 1: width, Item 2: height. </returns>
        /// <exception cref="InvalidOperationException"> When not initialized. </exception>
        public static (int, int) WindowResolution()
        {
            return _display?.WindowResolution() ?? throw new InvalidOperationException(ERR_NOT_INITIALIZED);
        }
        
        /// <summary>
        /// Gets the pixel resolution of the UI.
        /// It is intended that the stat bars, hands, and other GUI elements are at a smaller, low-resolution scale.
        /// </summary>
        /// <returns> Item 1: width, Item 2: height. </returns>
        /// <exception cref="InvalidOperationException"> When not initialized. </exception>
        public static (int, int) UIResolution()
        {
            return _display?.UIResolution() ?? throw new InvalidOperationException(ERR_NOT_INITIALIZED);
        }
    }
}
