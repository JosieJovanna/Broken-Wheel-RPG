namespace BrokenWheel.Core.Events
{
    public partial class TimeChangeEvent
    {
        /// <summary>
        /// Real time scale.
        /// </summary>
        public float RealTimeScale { get; }

        /// <summary>
        /// Effective calendar time scale.
        /// </summary>
        public float CalendarScale { get; }

        public TimeChangeEvent(float realTimeScale, float calendarTimeScale)
        {
            RealTimeScale = realTimeScale;
            CalendarScale = calendarTimeScale;
        }
    }
}
