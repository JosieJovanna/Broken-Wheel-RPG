using System;
using BrokenWheel.Core.Events.Stats;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar
{
    public class StatBar : IStatBar
    {
        private readonly IComplexStatistic _stat;
        private readonly IStatBarDisplay _display;
        private readonly StatBarSettings _settings;
        
        /// <summary>
        /// Whether the stat bar is hidden. This is different from <see cref="IsHidden"/>,
        /// because there may be an animation that takes time in the graphical implementation.
        /// This controls whether to start or stop hiding.
        /// </summary>
        private bool _isHiding;
        private int _pointsPerPixel;
        private int _x;
        private int _y;

        public StatBar(IComplexStatistic stat, IStatBarDisplay display, StatBarSettings settings)
        {
            _stat = stat ?? throw new ArgumentNullException(nameof(stat));
            _display = display ?? throw new ArgumentNullException(nameof(display));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
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

        public void SetPosition(int xPosition, int yPosition)
        {
            _x = xPosition;
            _y = yPosition;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            throw new NotImplementedException();
        }
    }
}
