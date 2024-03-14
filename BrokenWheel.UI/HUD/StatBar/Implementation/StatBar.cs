using System;
using BrokenWheel.Core.Stats.Info;
using BrokenWheel.UI.Display;
using BrokenWheel.UI.Settings.StatBar;
using BrokenWheel.UI.Settings.StatBar.Enum;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    /// <summary>
    /// An object which controls the GUI elements conveying a statistic.
    /// </summary>
    internal abstract partial class StatBar
    {
        private readonly IDisplayTool _display;

        public delegate void ReportPointsPerPixel(double ratio);
        public delegate double HighestPointsPerPixel();

        protected readonly StatBarSettings Settings;
        protected readonly ReportPointsPerPixel ReportPpp;
        protected readonly HighestPointsPerPixel HighestPpp;

        protected int X;
        protected int Y;

        public StatInfo Info { get; }
        public IStatBarDisplay Display { get; }
        public int Order { get; set; }
        public int HandlerID { get; set; }

        protected StatBar(
            StatBarSettings statBarSettings,
            IDisplayTool displayTool,
            StatInfo statInfo,
            IStatBarDisplay display,
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel,
            int order)
        {
            Settings = statBarSettings ?? throw new ArgumentNullException(nameof(statBarSettings));
            _display = displayTool ?? throw new ArgumentNullException(nameof(displayTool));
            Info = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
            Display = display ?? throw new ArgumentNullException(nameof(display));
            ReportPpp = reportPointsPerPixel ?? throw new ArgumentNullException(nameof(reportPointsPerPixel));
            HighestPpp = highestPointsPerPixel ?? throw new ArgumentNullException(nameof(highestPointsPerPixel));
            Order = order;
        }

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

        protected int CalculateYAdjustingForLengthIfOnTop(int length)
        {
            return Settings.DisplayCorner == StatBarCorner.TopLeft
                   || Settings.DisplayCorner == StatBarCorner.TopRight
                ? Y - length
                : Y;
        }

        protected int MaxLength() => System.Math.Min(Settings.MaxLength, ConstrainingDimension());

        protected int ConstrainingDimension()
        {
            return Settings.IsVertical
                ? _display.ScaledResolution.Height
                : _display.ScaledResolution.Width;
        }
    }
}
