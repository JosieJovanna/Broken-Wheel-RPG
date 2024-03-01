using System;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Core.Stats.Info;
using BrokenWheel.Math.Utility;
using BrokenWheel.UI.Display;
using BrokenWheel.UI.Settings.StatBar;
using BrokenWheel.UI.Settings.StatBar.Enum;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    internal sealed class SimpleStatBar : StatBar, IEventHandler<StatUpdatedEvent>
    {
        private readonly IStatBarDisplay _display;
        private Statistic _stat; // changed each update

        /// <summary>
        /// Initiates the object controlling the display, then immediately calls <see cref="UpdateDisplay"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> When these parameters are null. </exception>
        public SimpleStatBar(
            StatBarSettings statBarSettings,
            IDisplayTool displayTool,
            IStatBarDisplay simpleStatBarDisplay,
            StatInfo statInfo,
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel,
            int order = 99)
            : base(
                  statBarSettings,
                  displayTool,
                  statInfo,
                  simpleStatBarDisplay,
                  reportPointsPerPixel,
                  highestPointsPerPixel,
                  order)
        {
            _display = simpleStatBarDisplay;
            UpdateDisplay();
        }

        public void HandleEvent(StatUpdatedEvent gameEvent)
        {
            _stat = gameEvent.Statistic;
            UpdateDisplay();
        }

        protected override void UpdateDisplay()
        {
            var ppp = CalculatePointsPerPixel();
            var length = MathUtil.RaiseDoubleToInt(ppp * StatMax());
            var y = CalculateYAdjustingForLengthIfOnTop(length);
            var parameters = new UpdateDisplayParameters<Statistic>(Settings, _stat, ppp, length, X, y);
            SimpleStatBarDisplayUpdater.UpdateDisplay(_display, parameters);
        }

        private double CalculatePointsPerPixel()
        {
            switch (Settings.DisplayMode)
            {
                case StatBarDisplayMode.UniformPointsPerPixel:
                    return CalculateUniformPpp();
                case StatBarDisplayMode.UniformPointsPerPixelUntilMaxLength:
                    return CalculateUniformPppUpToMaxLength();
                case StatBarDisplayMode.FixedLengthBeforeMod:
                case StatBarDisplayMode.FixedLength:
                default:
                    return (double)StatMax() / Settings.DefaultLength;
            }
        }

        private double CalculateUniformPpp()
        {
            var ppp = Settings.DefaultPointPerPixelRatio; // prevent race condition
            var length = MaxLength(); // prevent race condition
            if (StatMax() * ppp < length)
                return System.Math.Max(ppp, HighestPpp());
            var newPpp = (double)StatMax() / length;
            ReportPpp(newPpp);
            return newPpp;
        }

        private double CalculateUniformPppUpToMaxLength()
        {
            var maxLength = System.Math.Min(Settings.MaxLength, ConstrainingDimension());
            var ppp = Settings.DefaultPointPerPixelRatio; // prevent race conditions
            return System.Math.Ceiling(ppp * StatMax()) > maxLength
                ? (double)StatMax() / maxLength
                : ppp;
        }

        private int StatMax() => _stat.Maximum >= 0 ? _stat.Maximum : Settings.MaximumWhenSimpleStatHasNone;
    }
}
