using System.Collections.Generic;
using BrokenWheel.Core.Settings.Registration;

namespace BrokenWheel.Core.Settings
{
    public sealed class StatBarSettings : ISettings
    {
        public StatBarDisplayMode DisplayMode { get; internal set; } = StatBarDisplayMode.FixedWidth;
        public bool IsVertical { get; internal set; } = true;
        public int StatBarX { get; internal set; } = 2;
        public int StatBarY { get; internal set; } = 2;
        public int MaxWidth { get; internal set; } = 320;
        public int StatBarLength { get; internal set; } = 100;
        public int StatBarThickness { get; internal set; } = 4;
        public int BorderSize { get; internal set; } = 1;
        public int Spacing { get; internal set; } = 2;
        
        public string[] MainStatOrder { get; internal set; } = { "HP", "SP", "WP" };
        public StatBarColorSettings HpColors { get; internal set; }
        public StatBarColorSettings SpColors { get; internal set; }
        public StatBarColorSettings WpColors { get; internal set; }
        public StatBarColorSettings DefaultColors { get; internal set; }
        public IDictionary<string, StatBarColorSettings> ColorsByCode { get; internal set; } = 
            new Dictionary<string, StatBarColorSettings>();
    }
}
