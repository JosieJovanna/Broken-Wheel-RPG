using BrokenWheel.Core.Settings.Registration;

namespace BrokenWheel.Core.Settings
{
    public sealed class StatBarSettings : ISettings
    {
        public StatBarDisplayMode DisplayMode { get; internal set; }
        public bool IsVertical { get; internal set; }
        public int MaxWidth { get; internal set; }
        public int StatBarLength { get; internal set; }
        public int StatBarThickness { get; internal set; }
        public int BorderSize { get; internal set; }
        public int Spacing { get; internal set; }
        public string[] MainStatOrder { get; internal set; }
    }
}
