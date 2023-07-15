using System;
using BrokenWheel.Core.Events.Stats;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Math.Utility;
using BrokenWheel.UI.Display;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal class StatBar : IStatBar
    {
        public delegate void ReportPointsPerPixel(double ratio);
        public delegate double HighestPointsPerPixel();
        
        private readonly StatBarSettings _settings;
        private readonly IComplexStatistic _stat;
        private readonly ReportPointsPerPixel _reportPpp;
        private readonly HighestPointsPerPixel _highestPpp;

        private bool _isHiding;
        private int _x;
        private int _y;

        /// <summary>
        /// Initiates the object controlling the display, then immediately calls <see cref="UpdateDisplay"/>.
        /// </summary>
        /// <param name="statBarSettings"> The statBarSettings for stat bars. </param>
        /// <param name="complexStatisticToTrack"> The complex statistic being represented. </param>
        /// <param name="statBarDisplay"> The GUI element this object controls. </param>
        /// <param name="reportPointsPerPixel"> A delegate to report back when the ratio of points per pixel changes. </param>
        /// <param name="highestPointsPerPixel"> A delegate which gets the highest ratio of points per pixel. </param>
        /// <exception cref="ArgumentNullException"> When these parameters are null. </exception>
        public StatBar(
            StatBarSettings statBarSettings,
            IComplexStatistic complexStatisticToTrack, 
            IStatBarDisplay statBarDisplay, 
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel)
        {
            _settings = statBarSettings ?? throw new ArgumentNullException(nameof(statBarSettings));
            _stat = complexStatisticToTrack ?? throw new ArgumentNullException(nameof(complexStatisticToTrack));
            Display = statBarDisplay ?? throw new ArgumentNullException(nameof(statBarDisplay));
            _reportPpp = reportPointsPerPixel ?? throw new ArgumentNullException(nameof(reportPointsPerPixel));
            _highestPpp = highestPointsPerPixel ?? throw new ArgumentNullException(nameof(highestPointsPerPixel));
            UpdateDisplay();
        }

        public IStatBarDisplay Display { get; }
        public StatTypeInfo Info { get => _stat.Info; }
        public bool IsHidden { get => Display.IsHidden; }

        public void Show()
        {
            if (!_isHiding)
                return;
            UpdateDisplay();
            Display.Show();
            _isHiding = false;
        }

        public void Hide()
        {
            if (_isHiding)
                return;
            Display.Hide();
            _isHiding = true;
        }

        public void StatUpdateHandler(object sender, StatUpdateEventArgs args)
        {
            if (!args.IsComplexStat())
                throw new InvalidOperationException(
                    $"Stat bars only support complex stats - '{args.Stat.Info.Name}' is not complex.");
            //_stat = args.StatAsComplex(); TODO: Return to this. Unsure if reference types are sufficient.
            UpdateDisplay();
        }

        public void Update(int xPosition, int yPosition)
        {
            _x = xPosition;
            _y = yPosition;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var parameters = UpdateDisplayParameters();
            UpdateBorder(parameters);
            UpdateBackground(parameters);
            UpdatePrimary(parameters, out var primaryLength);
            UpdateSecondary(parameters, primaryLength);
            UpdateExhaustion(parameters);
        }

        #region Display Calculations
        private UpdateDisplayParameters UpdateDisplayParameters()
        {
            var ppp = CalculatePointsPerPixel();
            var length = MathUtil.RaiseDoubleToInt(ppp * _stat.EffectiveMaximum);
            var y = CalculateYAdjustingForLengthIfOnTop(length);
            return new UpdateDisplayParameters(_settings, _stat, ppp, length, _x, y);
        }

        private int CalculateYAdjustingForLengthIfOnTop(int length)
        {
            return _settings.DisplayCorner == StatBarCorner.TopLeft 
                   || _settings.DisplayCorner == StatBarCorner.TopRight
                ? _y - length
                : _y;
        }

        private double CalculatePointsPerPixel()
        {
            switch (_settings.DisplayMode)
            {
                case StatBarDisplayMode.FixedLengthBeforeMod:
                    return CalculateFixedPppBeforeMod();
                case StatBarDisplayMode.UniformPointsPerPixel:
                    return CalculateUniformPpp();
                case StatBarDisplayMode.UniformPointsPerPixelUntilMaxLength:
                    return CalculateUniformPppUpToMax();
                case StatBarDisplayMode.FixedLength:
                default:
                    return (double)_stat.EffectiveMaximum / _settings.DefaultLength;
            }
        }

        private double CalculateFixedPppBeforeMod()
        {
            var length = _settings.DefaultLength; // prevent race condition
            var basePpp = (double)_stat.Maximum / length;
            var modLength = MathUtil.RaiseDoubleToInt(basePpp * _stat.Modifier);
            length = System.Math.Min(length + modLength, GetConstrainingDimension());
            return (double)_stat.EffectiveMaximum / length;
        }

        private double CalculateUniformPpp()
        {
            var ppp = _settings.DefaultPointPerPixelRatio; // prevent race condition
            var max = GetMaxWidth(); // prevent race condition
            if (_stat.EffectiveMaximum * ppp < max)
                return System.Math.Max(ppp, _highestPpp());
            var newPpp = (double)_stat.EffectiveMaximum / max;
            _reportPpp(newPpp);
            return newPpp;
        }

        private double CalculateUniformPppUpToMax()
        {
            var maxLength = System.Math.Min(_settings.MaxLength, GetConstrainingDimension());
            var ppp = _settings.DefaultPointPerPixelRatio; // prevent race conditions
            return System.Math.Ceiling(ppp * _stat.EffectiveMaximum) > maxLength
                ? (double)_stat.EffectiveMaximum / maxLength
                : ppp;
        }

        private int GetMaxWidth() => System.Math.Min(_settings.MaxLength, GetConstrainingDimension());

        /// <summary>
        /// Gets either the width or height of the UI, depending on whether stat bars are vertical enough,
        /// as that is the maximum length a stat bar can have, regardless of settings.
        /// </summary>
        private int GetConstrainingDimension() => _settings.IsVertical ? DisplayInfo.UIHeight() : DisplayInfo.UIWidth();
        #endregion

        #region Updates
        private void UpdateBorder(UpdateDisplayParameters p)
        {
            var borderSizeX2 = p.BorderSize * 2;
            var length = p.FullLength + borderSizeX2;
            var thickness = p.Thickness + borderSizeX2;
            var width = p.IsVertical ? thickness : length;
            var height = p.IsVertical ? length : thickness;
            Display.SetBorderDimensions(p.BaseX, p.BaseY, width, height);
        }

        private void UpdateBackground(UpdateDisplayParameters p)
        {
            var width = p.IsVertical ? p.Thickness : p.FullLength;
            var height = p.IsVertical ? p.FullLength : p.Thickness;
            Display.SetBackgroundDimensions(p.BackgroundX, p.BackgroundY, width, height);
        }

        private void UpdatePrimary(UpdateDisplayParameters p, out int length)
        {
            length = 10; // TODO: current value/destination value
            var width = p.IsVertical ? p.Thickness : length;
            var height = p.IsVertical ? length : p.Thickness;
            Display.SetPrimaryDimensions(p.BackgroundX, p.BackgroundY, width, height);
        }

        private void UpdateSecondary(UpdateDisplayParameters p, int primaryLength)
        {
            var length = 5; // TODO: destination value/current value
            var (x, y, width, height) = p.IsVertical 
                ? VerticalSecondaryDimensions(p, primaryLength, length) 
                : HorizontalSecondaryDimensions(p, primaryLength, length);
            Display.SetSecondaryDimensions(x, y, width, height);
        }

        private static (int, int, int, int) VerticalSecondaryDimensions(UpdateDisplayParameters p, int primaryLength, int length)
        {
            var x = p.BackgroundX;
            var y = p.BackgroundY + primaryLength + length;
            var width = p.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalSecondaryDimensions(UpdateDisplayParameters p, int primaryLength, int length)
        {
            var x = p.BackgroundX + primaryLength + length;
            var y = p.BackgroundY;
            var width = length;
            var height = p.Thickness;
            return (x, y, width, height);
        }

        private void UpdateExhaustion(UpdateDisplayParameters p)
        {
            var length = MathUtil.RaiseDoubleToInt(p.PPP * p.Stat.Exhaustion);
            var (x, y, width, height) = p.IsVertical
                ? VerticalExhaustionDimensions(p, length)
                : HorizontalExhaustionDimensions(p, length);
            Display.SetExhaustionDimensions(x, y, width, height);
        }
        

        private static (int, int, int, int) VerticalExhaustionDimensions(UpdateDisplayParameters p, int length)
        {
            var x = p.BackgroundX;
            var y = p.BackgroundY + p.FullLength - length;
            var width = p.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalExhaustionDimensions(UpdateDisplayParameters p, int length)
        {
            var x = p.BackgroundX + p.FullLength - length;
            var y = p.BackgroundY;
            var width = length;
            var height = p.Thickness;
            return (x, y, width, height);
        }
        #endregion
    }
}
