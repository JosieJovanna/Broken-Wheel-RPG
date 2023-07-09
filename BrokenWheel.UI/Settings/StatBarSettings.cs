using BrokenWheel.Core.Settings;
using BrokenWheel.UI.StatBar;

namespace BrokenWheel.UI.Settings
{
    public sealed class StatBarSettings : ISettings
    {
        public StatBarDisplayMode DisplayMode { get; set; }
        public bool IsVertical { get; set; }
        public int MaxWidth { get; set; }
        public int StatBarLength { get; set; }
        public int StatBarThickness { get; set; }
        public int BorderSize { get; set; }
        public int Spacing { get; set; }
    }
}
