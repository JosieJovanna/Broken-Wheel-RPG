using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.Time.Implementation
{
    public sealed partial class FullTimeService : ITimeTicker
    {
        private readonly ILogger _logger;
        private readonly TimeSettings _timeSettings;
        private readonly IEventSubject<TimeChangeEvent> _timeChangeSubject;

        private TimeFunction _tickTimeFxs;
        private TimeFunction _realTimeFxs;
        private TimeFunction _calendarTimeFxs;

        public FullTimeService(
            ILogger logger,
            TimeSettings timeSettings,
            IEventAggregator eventAggregator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeSettings = timeSettings ?? throw new ArgumentNullException(nameof(timeSettings));
            _timeChangeSubject = eventAggregator?.GetSubject<TimeChangeEvent>() ?? throw new ArgumentNullException(nameof(eventAggregator));

            ResetTimeScale();
            ResetCalendarScale();
        }

        public void Tick(double delta)
        {
            TickTime(delta);
            RealTime(delta);
            CalendarTime(delta);
        }

        private void TickTime(double delta)
        {
            _tickTimeFxs?.Invoke(delta);
        }

        private void RealTime(double delta)
        {
            var realDelta = delta * TimeScale;
            _realTimeFxs?.Invoke(realDelta);
        }

        private void CalendarTime(double delta)
        {
            var calendarDelta = delta * EffectiveCalendarTimeScale;
            _calendarTimeFxs?.Invoke(calendarDelta);
        }
    }
}
