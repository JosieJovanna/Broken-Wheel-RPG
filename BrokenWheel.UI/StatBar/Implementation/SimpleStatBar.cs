using System;
using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;

namespace BrokenWheel.UI.StatBar.Implementation
{
    public class SimpleStatBar : IStatBar, IListener<SimpleStatUpdatedEvent>
    {

        /// <summary>
        /// Initiates the object controlling the display, then immediately calls <see cref="UpdateDisplay"/>.
        /// </summary>
        /// <param name="statBarSettings"> The statBarSettings for stat bars. </param>
        /// <param name="complexStatBarDisplay"> The GUI element this object controls. </param>
        /// <param name="statInfo"> The stat this stat bar is assigned to track. </param>
        /// <param name="reportPointsPerPixel"> A delegate to report back when the ratio of points per pixel changes. </param>
        /// <param name="highestPointsPerPixel"> A delegate which gets the highest ratio of points per pixel. </param>
        /// <param name="order"> The order in which the stat bar is displayed. </param>
        /// <exception cref="ArgumentNullException"> When these parameters are null. </exception>
        public SimpleStatBar(
            StatBarSettings statBarSettings,
            IStatBarUIElement complexStatBarDisplay,
            StatInfo statInfo,
            ComplexStatBar.ReportPointsPerPixel reportPointsPerPixel,
            ComplexStatBar.HighestPointsPerPixel highestPointsPerPixel,
            int order = 99)
        {
            _settings = statBarSettings ?? throw new ArgumentNullException(nameof(statBarSettings));
            Display = complexStatBarDisplay ?? throw new ArgumentNullException(nameof(complexStatBarDisplay));
            Info = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
            _reportPpp = reportPointsPerPixel ?? throw new ArgumentNullException(nameof(reportPointsPerPixel));
            _highestPpp = highestPointsPerPixel ?? throw new ArgumentNullException(nameof(highestPointsPerPixel));
            Order = order;
            UpdateDisplay();
        }

        public IStatBarUIElement Display { get; }
        public StatInfo Info { get; }
        public bool IsHidden { get; }
        public int Order { get; set; }
        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void SetPosition(int xPosition, int yPosition)
        {
            throw new System.NotImplementedException();
        }

        public void HandleEvent(SimpleStatUpdatedEvent gameEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
