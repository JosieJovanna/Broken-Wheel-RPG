using System.Drawing;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.UI.Settings.StatBar
{
    public class StatBarColorSettings : ISettings
    {
        public Color BorderColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }
        public Color ExhaustionColor { get; set; }
    }
}
