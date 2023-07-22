using System;
using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal sealed class SimpleStatBar : StatBar, IListener<SimpleStatUpdatedEvent>
    {
        private readonly IStatBarDisplay _display;
        private StatUpdate _stat;
        
        /// <summary>
        /// Initiates the object controlling the display, then immediately calls <see cref="UpdateDisplay"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> When these parameters are null. </exception>
        public SimpleStatBar(
            StatBarSettings statBarSettings,
            IStatBarDisplay simpleStatBarDisplay,
            StatInfo statInfo,
            ReportPointsPerPixel reportPointsPerPixel,
            HighestPointsPerPixel highestPointsPerPixel,
            int order = 99)
            : base(statBarSettings, statInfo, simpleStatBarDisplay, reportPointsPerPixel, highestPointsPerPixel, order)
        {
            _display = simpleStatBarDisplay;
            UpdateDisplay();
        }

        public void HandleEvent(SimpleStatUpdatedEvent gameEvent)
        {
            _stat = gameEvent.Stat;
            throw new System.NotImplementedException();
        }

        protected override void UpdateDisplay()
        {
            throw new NotImplementedException();
        }
    }
}
