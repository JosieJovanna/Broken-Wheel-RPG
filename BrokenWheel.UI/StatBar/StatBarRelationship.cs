﻿using System;
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

        public StatBarRelationship(
            StatBarSettings statBarSettings, 
            IComplexStatistic complexStatistic, 
            IStatBarDisplay statBarDisplay, 
            StatBar.ReportPointsPerPixel reportPointsPerPixelDelegate,
            StatBar.HighestPointsPerPixel highestPointsPerPixelDelegate,
            int order = 99)
        {
            if (statBarSettings == null)
                statBarSettings = SettingsRegistry.GetSettings<StatBarSettings>();
            if (complexStatistic == null)
                throw new ArgumentNullException(nameof(complexStatistic));
            if (statBarDisplay == null)
                throw new ArgumentNullException(nameof(statBarDisplay));
            if (reportPointsPerPixelDelegate == null)
                throw new ArgumentNullException(nameof(reportPointsPerPixelDelegate));
            if (highestPointsPerPixelDelegate == null)
                throw new ArgumentNullException(nameof(highestPointsPerPixelDelegate));
            
            Order = order;
            StatBar = new StatBar(
                statBarSettings, 
                complexStatistic, 
                statBarDisplay,
                reportPointsPerPixelDelegate,
                highestPointsPerPixelDelegate);
        }
    }
}
