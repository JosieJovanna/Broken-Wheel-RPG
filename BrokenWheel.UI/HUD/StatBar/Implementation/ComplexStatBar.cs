using System;
using BrokenWheel.Core.Event.Handling;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    internal sealed class ComplexStatBar : StatBar, IEventHandler<ComplexStatUpdatedEvent>
    {
        private readonly IComplexStatBarDisplay _display;
        private ComplexStat _stat;
        
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
            var parameters = new UpdateDisplayParameters<ComplexStat>(Settings, _stat, ppp, length, X, y);
            ComplexStatBarDisplayUpdater.UpdateDisplay(_display, parameters);
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
                    return CalculateUniformPppUpToMaxLength();
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
            length = System.Math.Min(length + modLength, ConstrainingDimension());
            return (double)_stat.EffectiveMaximum / length;
        }

        private double CalculateUniformPpp()
        {
            var ppp = Settings.DefaultPointPerPixelRatio; // prevent race condition
            var max = MaxLength(); // prevent race condition
            if (_stat.EffectiveMaximum * ppp < max)
                return System.Math.Max(ppp, HighestPpp());
            var newPpp = (double)_stat.EffectiveMaximum / max;
            ReportPpp(newPpp);
            return newPpp;
        }

        private double CalculateUniformPppUpToMaxLength()
        {
            var maxLength = System.Math.Min(Settings.MaxLength, ConstrainingDimension());
            var ppp = Settings.DefaultPointPerPixelRatio; // prevent race conditions
            return System.Math.Ceiling(ppp * _stat.EffectiveMaximum) > maxLength
                ? (double)_stat.EffectiveMaximum / maxLength
                : ppp;
        }
    }
}
