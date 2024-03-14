namespace BrokenWheel.Core.Time
{
    public partial class TimeChangeEvent
    {
        public float RealTimeScale { get; }
        public float CalendarScale { get; }
        public bool IsRealTimePaused { get; }
        public bool IsCalendarPaused { get; }

        public TimeChangeEvent(
            float realTimeScale,
            float calendarTimeScale,
            bool isRealTimePaused,
            bool isCalendarPaused)
        {
            RealTimeScale = realTimeScale;
            CalendarScale = calendarTimeScale;
            IsRealTimePaused = isRealTimePaused;
            IsCalendarPaused = isCalendarPaused;
        }
    }
}
