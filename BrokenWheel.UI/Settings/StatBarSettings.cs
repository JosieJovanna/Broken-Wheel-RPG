using BrokenWheel.Core.Settings;
using BrokenWheel.UI.StatBar;

namespace BrokenWheel.UI.Settings
{
    public class StatBarSettings : ISettings
    {
        public StatBarDisplayMode DisplayMode { get; set; }
        public bool IsVertical { get; set; }
        public int MaxWidth { get; set; }
        public int StatBarWidth { get; set; }
        public int StatBarHeight { get; set; }
        public int BorderSize { get; set; }
        public int BorderWidth { get; set; }
        public int Spacing { get; set; }
    }
}
