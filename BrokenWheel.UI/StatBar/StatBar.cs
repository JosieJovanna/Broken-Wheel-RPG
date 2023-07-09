using System;
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
        
        private int _x;
        private int _y;
        private int _pointsPerPixel;

        public StatBar(IComplexStatistic stat, IStatBarDisplay display, StatBarSettings settings, int x, int y)
        {
            _stat = stat ?? throw new ArgumentNullException(nameof(stat));
            _display = display ?? throw new ArgumentNullException(nameof(display));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _x = x;
            _y = y;
            UpdateDisplay();
        }

        public StatTypeInfo Type { get => _stat.Type; }

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
