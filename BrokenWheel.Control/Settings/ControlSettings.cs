using BrokenWheel.Core.Settings;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.Control.Settings
{
    public class ControlSettings : ISettings
    {
        /// <summary>
        /// The base speed at which the camera rotates.
        /// </summary>
        public float BaseLookSpeed { get; set; } = 0.02f;

        public float CameraPanSensitivity { get; set; } = 1.7f;
        public float CameraTiltSensitivity { get; set; } = 1.5f;


        public bool InvertLookVertical { get; set; } = false;
        public float LowestCameraTilt { get; set; } = AngleUtil.DegreesToRadians(-85);
        public float HighestCameraTilt { get; set; } = AngleUtil.DegreesToRadians(90);
    }
}
