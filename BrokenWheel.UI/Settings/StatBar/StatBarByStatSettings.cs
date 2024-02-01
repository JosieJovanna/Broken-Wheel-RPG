using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.Settings.StatBar
{
    public class StatBarByStatSettings : ISettings
    {
        public Stat Stat { get; set; }
        public StatBarColorSettings ColorSettings { get; set; }
    }
}
