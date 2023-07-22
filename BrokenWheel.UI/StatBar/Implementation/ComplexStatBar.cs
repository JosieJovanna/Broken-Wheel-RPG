using System;
using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Math.Utility;
using BrokenWheel.UI.Display;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal sealed class ComplexStatBar : StatBar, IListener<ComplexStatUpdatedEvent>
    {
        private readonly IComplexStatBarDisplay _display;
        private ComplexStatUpdate _stat;
        
        /// <summary>
        /// Initiates the object controlling the display, then immediately calls <see cref="UpdateDisplay"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> When these parameters are null. </exception>
        public ComplexStatBar(
            StatBarSettings statBarSettings,
            IComplexStatBarDisplay complexStatBarDisplay,
            StatInfo statInfo,
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel,
            int order = 99) 
            : base(statBarSettings, statInfo, complexStatBarDisplay, reportPointsPerPixel, highestPointsPerPixel, order)
        {
            _display = complexStatBarDisplay;
            UpdateDisplay();
        }

        public void HandleEvent(ComplexStatUpdatedEvent gameEvent)
        {
            _stat = gameEvent.Stat;
            UpdateDisplay();
        }

        protected override void UpdateDisplay()
        {
            var ppp = CalculatePointsPerPixel();
            var length = MathUtil.RaiseDoubleToInt(ppp * _stat.EffectiveMaximum);
            var y = CalculateYAdjustingForLengthIfOnTop(length);
            var parameters = new UpdateDisplayParameters<ComplexStatUpdate>(Settings, _stat, ppp, length, X, y);
            ComplexStatBarDisplayUpdater.UpdateDisplay(_display, parameters);
        }

        private int CalculateYAdjustingForLengthIfOnTop(int length)
        {
            return Settings.DisplayCorner == StatBarCorner.TopLeft 
                   || Settings.DisplayCorner == StatBarCorner.TopRight
                ? Y - length
                : Y;
        }

        private double CalculatePointsPerPixel()
        {
            switch (Settings.DisplayMode)
            {
                case StatBarDisplayMode.FixedLengthBeforeMod:
                    return CalculateFixedPppBeforeMod();
                case StatBarDisplayMode.UniformPointsPerPixel:
                    return CalculateUniformPpp();
                case StatBarDisplayMode.UniformPointsPerPixelUntilMaxLength:
                    return CalculateUniformPppUpToMax();
                case StatBarDisplayMode.FixedLength:
                default:
                    return (double)_stat.EffectiveMaximum / Settings.DefaultLength;
            }
        }

        private double CalculateFixedPppBeforeMod()
        {
            var length = Settings.DefaultLength; // prevent race condition
            var basePpp = (double)_stat.Maximum / length;
            var modLength = MathUtil.RaiseDoubleToInt(basePpp * _stat.Modifier);
            length = System.Math.Min(length + modLength, GetConstrainingDimension());
            return (double)_stat.EffectiveMaximum / length;
        }

        private double CalculateUniformPpp()
        {
            var ppp = Settings.DefaultPointPerPixelRatio; // prevent race condition
            var max = GetMaxWidth(); // prevent race condition
            if (_stat.EffectiveMaximum * ppp < max)
                return System.Math.Max(ppp, HighestPpp());
            var newPpp = (double)_stat.EffectiveMaximum / max;
            ReportPpp(newPpp);
            return newPpp;
        }

        private double CalculateUniformPppUpToMax()
        {
            var maxLength = System.Math.Min(Settings.MaxLength, GetConstrainingDimension());
            var ppp = Settings.DefaultPointPerPixelRatio; // prevent race conditions
            return System.Math.Ceiling(ppp * _stat.EffectiveMaximum) > maxLength
                ? (double)_stat.EffectiveMaximum / maxLength
                : ppp;
        }

        private int GetMaxWidth() => System.Math.Min(Settings.MaxLength, GetConstrainingDimension());

        private int GetConstrainingDimension() => Settings.IsVertical ? DisplayInfo.UIHeight() : DisplayInfo.UIWidth();
    }
}
