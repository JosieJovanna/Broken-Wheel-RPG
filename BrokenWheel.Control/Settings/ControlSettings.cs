using BrokenWheel.Core.Settings;

namespace BrokenWheel.Control.Settings
{
    public partial class ControlSettings : ISettings
    {
        public CameraSettings Camera { get; set; } = new CameraSettings();

        public float ToggleStanceHoldTime { get; set; } = 0.85f;
        public float ToggleSpeedHoldTime { get; set; } = 0.85f;
    }
}
