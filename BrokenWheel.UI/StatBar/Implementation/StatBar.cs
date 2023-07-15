using System;
using BrokenWheel.Core.Events.Stats;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar.Implementation
{
    public class StatBar : IStatBar
    {
        public delegate void ReportPointsPerPixel(double ratio);
        public delegate int HighestPointsPerPixel();
        
        private readonly StatBarSettings _settings;
        private readonly IComplexStatistic _stat;
        private readonly IStatBarDisplay _display;
        private readonly ReportPointsPerPixel _reportPpp;
        private readonly HighestPointsPerPixel _highestPpp;

        private bool _isHiding;
        private int _pointsPerPixel;
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

        public void SetPositionAndUpdate(int xPosition, int yPosition)
        {
            _x = xPosition;
            _y = yPosition;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            throw new NotImplementedException();
        }

        private int CalculatePointsPerPixel(IComplexStatistic stat)
        {
            switch (_settings.DisplayMode)
            {
                case StatBarDisplayMode.FixedLengthBeforeMod:
                    
                case StatBarDisplayMode.UniformPointsPerPixel:
                    
                case StatBarDisplayMode.UniformPointsPerPixelUntilMaxLength:
                    
                case StatBarDisplayMode.FixedLength:
                default:
                    
            }
        }
    }
}
