namespace BrokenWheel.Core.Time.Implementation
{
    public sealed partial class FullTimeService : ITimeScaler
    {
        public void PauseRealTime() => IsRealTimePaused = true;

        public void PauseCalendar() => IsCalendarPaused = true;

        public void ResumeRealTime() => IsRealTimePaused = false;

        public void ResumeCalendar() => IsCalendarPaused = false;

        public void PausePhysics() => IsPhysicsPaused = true;

        public void ResumePhysics() => IsPhysicsPaused = false;


        public void SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            RecalculateEffectiveCalendarTimeScale();
            _logger.LogCategory("Time", $"Set time scale to {timeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        public void SetCalendarTimeScale(float timeScale)
        {
            CalendarTimeScale = timeScale;
            RecalculateEffectiveCalendarTimeScale();
            _logger.LogCategory("Time", $"Set calendar time scale to {timeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        public void ResetTimeScale()
        {
            TimeScale = _timeSettings.DefaultTimeScale;
            RecalculateEffectiveCalendarTimeScale();
            _logger.LogCategory("Time", $"Reset time scale to {TimeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        public void ResetCalendarScale()
        {
            CalendarTimeScale = _timeSettings.DefaultCalendarTimeScale;
            RecalculateEffectiveCalendarTimeScale();
            _logger.LogCategory("Time", $"Reset calendar time scale to {CalendarTimeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        private void RecalculateEffectiveCalendarTimeScale()
            => EffectiveCalendarTimeScale = CalendarTimeScale * TimeScale;
    }
}
