using BrokenWheel.Core.Settings;

namespace BrokenWheel.Control.Settings
{
    public class ControlSettings : ISettings
    {
        /// <summary>
        /// Movement comes between 0 and 1, then is multiplied against time and this base speed in m/s.
        /// </summary>
        public float BaseMoveSpeed { get; set; } = 2.6f;

        /// <summary>
        /// The base speed at which the camera rotates.
        /// </summary>
        public float BaseLookSpeed { get; set; } = 10;
    }
}
