using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;

namespace BrokenWheel.UI.StatBar
{
    internal class StatBarRelationship
    {
        public IStatBar Bar { get; }
        public IComplexStatistic Stat { get; }
        public int Order { get; set; }
        public StatType Type { get => Stat.Info.Type; }

        public StatBarRelationship(IComplexStatistic statistic, IStatBarDisplay display)
        {
            Bar = new StatBar(statistic, display, SettingsRegistry.GetSettings<StatBarSettings>());
            Stat = statistic;
        }
    }
}
