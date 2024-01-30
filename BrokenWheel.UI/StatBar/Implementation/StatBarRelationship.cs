using System;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal class StatBarRelationship
    {
        public IStatBar StatBar { get; }
        public StatInfo StatInfo { get; }
        public int Order { get; set; }

        public StatBarRelationship(
            StatBarSettings statBarSettings, 
            StatInfo statInfo, 
            IStatBarUIElement statBarUIElement, 
            StatBar.ReportPointsPerPixel reportPointsPerPixelDelegate,
            StatBar.HighestPointsPerPixel highestPointsPerPixelDelegate,
            int order = 99)
        {
            Order = order;
            StatInfo = statInfo;
            StatBar = new StatBar(
                statBarSettings ?? throw new ArgumentNullException(nameof(statBarSettings)), 
                statBarUIElement ?? throw new ArgumentNullException(nameof(statBarUIElement)),
                statInfo ?? throw new ArgumentNullException(nameof(statInfo)), 
                reportPointsPerPixelDelegate ?? throw new ArgumentNullException(nameof(reportPointsPerPixelDelegate)),
                highestPointsPerPixelDelegate ?? throw new ArgumentNullException(nameof(highestPointsPerPixelDelegate)));
        }
    }
}
