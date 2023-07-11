using System;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;

namespace BrokenWheel.UI.StatBar
{
    internal class StatBarRelationship
    {
        public IStatBar StatBar { get; }
        public IComplexStatistic Stat { get; }
        public int Order { get; set; }
        public StatType Type { get => Stat.Info.Type; }

        public StatBarRelationship(IComplexStatistic statistic, IStatBarDisplay display, int order = 99, StatBarSettings settings = null)
        {
            if (display == null)
                throw new ArgumentNullException(nameof(display));
            Stat = statistic ?? throw new ArgumentNullException(nameof(statistic));
            StatBar = new StatBar(statistic, display, settings ?? SettingsRegistry.GetSettings<StatBarSettings>());
        }
    }
}
