using System;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar.Implementation
{
    /// <summary>
    /// An object which controls the GUI elements conveying a statistic.
    /// </summary>
    internal abstract class StatBar
    {
        public delegate void ReportPointsPerPixel(double ratio);
        public delegate double HighestPointsPerPixel();
        
        protected readonly StatBarSettings Settings;
        protected readonly ReportPointsPerPixel ReportPpp;
        protected readonly HighestPointsPerPixel HighestPpp;

        protected int X;
        protected int Y;
        
        protected StatBar(
            StatBarSettings statBarSettings,
            StatInfo statInfo,
            IStatBarDisplay display,
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel,
            int order)
        {
            Settings = statBarSettings ?? throw new ArgumentNullException(nameof(statBarSettings));
            Info = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
            Display = display ?? throw new ArgumentNullException(nameof(display));
            ReportPpp = reportPointsPerPixel ?? throw new ArgumentNullException(nameof(reportPointsPerPixel));
            HighestPpp = highestPointsPerPixel ?? throw new ArgumentNullException(nameof(highestPointsPerPixel));
            Order = order;
        }
        
        public StatInfo Info { get; }
        public IStatBarDisplay Display { get; }
        public int Order { get; set; }

        /// <summary>
        /// Sets the position of the bar then updates the display according to its statistic.
        /// The class is in charge of mirroring top or bottom.
        /// </summary>
        public void SetPosition(int xPosition, int yPosition)
        {
            X = xPosition;
            Y = yPosition;
            UpdateDisplay();
        }

        protected abstract void UpdateDisplay();
    }
}
