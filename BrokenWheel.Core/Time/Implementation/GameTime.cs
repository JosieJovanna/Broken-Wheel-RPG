using BrokenWheel.Core.DependencyInjection;

namespace BrokenWheel.Core.Time.Implementation
{
    public static class GameTime
    {
        private static readonly ITimeService _service;

        static GameTime()
        {
            _service = Injection.GetModule().GetService<ITimeService>();
        }

        public static float TimeScale => _service.TimeScale;
        public static float CalendarTimeScale => _service.CalendarTimeScale;
        public static float EffectiveCalendarTimeScale => _service.EffectiveCalendarTimeScale;

        public static bool IsRealTimePaused => _service.IsRealTimePaused;
        public static bool IsCalendarPaused => _service.IsCalendarPaused;
        public static bool IsPhysicsPaused => _service.IsPhysicsPaused;

        public static void AddTickTimeFx(TimeFunction timeFx) => _service.AddTickTimeFx(timeFx);
        public static void AddRealTimeFx(TimeFunction timeFx) => _service.AddRealTimeFx(timeFx);
        public static void AddCalendarTimeFx(TimeFunction timeFx) => _service.AddCalendarTimeFx(timeFx);
    }
}
