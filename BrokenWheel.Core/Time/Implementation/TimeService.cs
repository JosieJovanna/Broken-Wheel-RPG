namespace BrokenWheel.Core.Time.Implementation
{
    public sealed partial class FullTimeService : ITimeService
    {
        public float TimeScale { get; private set; }

        public float CalendarTimeScale { get; private set; }

        public float EffectiveCalendarTimeScale { get; private set; }

        public bool IsRealTimePaused { get; private set; }

        public bool IsCalendarPaused { get; private set; }

        public bool IsPhysicsPaused { get; private set; }

        public bool IsAllTimeflowPaused { get; private set; }

        public void AddTickTimeFx(TimeFunction timeFx)
        {
            _tickTimeFxs += timeFx;
            _logger.LogCategory("Time", $"Added tick-time function: `{timeFx.Method.Name}`");
        }

        public void RemoveTickTimeFx(TimeFunction timeFx)
        {
            _tickTimeFxs -= timeFx;
            _logger.LogCategory("Time", $"Removed tick-time function: `{timeFx.Method.Name}`");
        }

        public void AddRealTimeFx(TimeFunction timeFx)
        {
            _realTimeFxs += timeFx;
            _logger.LogCategory("Time", $"Added real-time function: `{timeFx.Method.Name}`");
        }

        public void RemoveRealTimeFx(TimeFunction timeFx)
        {
            _realTimeFxs -= timeFx;
            _logger.LogCategory("Time", $"Removed real-time function: `{timeFx.Method.Name}`");
        }

        public void AddCalendarTimeFx(TimeFunction timeFx)
        {
            _calendarTimeFxs += timeFx;
            _logger.LogCategory("Time", $"Added calendar-time function: `{timeFx.Method.Name}`");
        }

        public void RemoveCalendarTimeFx(TimeFunction timeFx)
        {
            _calendarTimeFxs -= timeFx;
            _logger.LogCategory("Time", $"Removed calendar-time function: `{timeFx.Method.Name}`");
        }
    }
}
