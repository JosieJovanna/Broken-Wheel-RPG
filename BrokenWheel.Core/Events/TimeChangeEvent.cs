namespace BrokenWheel.Core.Events
{
    public partial class TimeChangeEvent : UncategorizedGameEvent
    {
        /// <summary>
        /// Real time scale.
        /// </summary>
        public float RealTimeScale { get; }

        /// <summary>
        /// Effective calendar time scale.
        /// </summary>
        public float CalendarScale { get; }

        public TimeChangeEvent(object sender, float realTimeScale, float calendarTimeScale)
            : base(sender)
        {
            RealTimeScale = realTimeScale;
            CalendarScale = calendarTimeScale;
        }
    }
}
