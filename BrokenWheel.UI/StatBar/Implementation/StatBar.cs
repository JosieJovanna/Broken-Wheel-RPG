using System;
using BrokenWheel.Core.Events.Stats;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Math.Utility;
using BrokenWheel.UI.Display;

namespace BrokenWheel.UI.StatBar.Implementation
{
    public class StatBar : IStatBar
    {
        public delegate void ReportPointsPerPixel(double ratio);
        public delegate double HighestPointsPerPixel();
        
        private readonly StatBarSettings _settings;
        private readonly IComplexStatistic _stat;
        private readonly IStatBarDisplay _display;
        private readonly ReportPointsPerPixel _reportPpp;
        private readonly HighestPointsPerPixel _highestPpp;

        private bool _isHiding;
        private int _x;
        private int _y;

        /// <summary>
        /// Initiates the object controlling the display, then immediately calls <see cref="UpdateDisplay"/>.
        /// </summary>
        /// <param name="settings"> The settings for stat bars. If null, will attempt to fetch them. </param>
        /// <param name="stat"> The complex statistic being represented. </param>
        /// <param name="display"> The GUI element this object controls. </param>
        /// <param name="reportPointsPerPixel"> A delegate to report back when the ratio of points per pixel changes. </param>
        /// <param name="highestPointsPerPixel"> A delegate which gets the highest ratio of points per pixel. </param>
        /// <exception cref="ArgumentNullException"> When these parameters are null. </exception>
        public StatBar(
            StatBarSettings settings,
            IComplexStatistic stat, 
            IStatBarDisplay display, 
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel)
        {
            _settings = settings ?? SettingsRegistry.GetSettings<StatBarSettings>();
            _stat = stat ?? throw new ArgumentNullException(nameof(stat));
            _display = display ?? throw new ArgumentNullException(nameof(display));
            _reportPpp = reportPointsPerPixel ?? throw new ArgumentNullException(nameof(reportPointsPerPixel));
            _highestPpp = highestPointsPerPixel ?? throw new ArgumentNullException(nameof(highestPointsPerPixel));
            UpdateDisplay();
        }

        public StatTypeInfo Info { get => _stat.Info; }
        public bool IsHidden { get => _display.IsHidden; }

        public void Show()
        {
            if (!_isHiding)
                return;
            UpdateDisplay();
            _display.Show();
            _isHiding = false;
        }

        public void Hide()
        {
            if (_isHiding)
                return;
            _display.Hide();
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
            var ppp = CalculatePointsPerPixel();
            throw new NotImplementedException();
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
            length = System.Math.Min(length + modLength, GetConstrainingUIDimension());
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
            var maxLength = System.Math.Min(_settings.MaxLength, GetConstrainingUIDimension());
            var ppp = _settings.DefaultPointPerPixelRatio; // prevent race conditions
            return System.Math.Ceiling(ppp * _stat.EffectiveMaximum) > maxLength
                ? (double)_stat.EffectiveMaximum / maxLength
                : ppp;
        }

        private int GetMaxWidth() => System.Math.Min(_settings.MaxLength, GetConstrainingUIDimension());

        /// <summary>
        /// Gets either the width or height of the UI, depending on whether stat bars are vertical enough,
        /// as that is the maximum length a stat bar can have, regardless of settings.
        /// </summary>
        private int GetConstrainingUIDimension() => _settings.IsVertical ? DisplayInfo.UIHeight() : DisplayInfo.UIWidth();
    }
}
