namespace BrokenWheel.Core.Time.Implementation
{
    public sealed partial class FullTimeService : ITimeScaler
    {
        public void PauseRealTime()
        {
            IsRealTimePaused = true;
            EmitChangeEvent();
        }

        public void PauseCalendar()
        {
            IsCalendarPaused = true;
            EmitChangeEvent();
        }

        public void ResumeRealTime()
        {
            IsRealTimePaused = false;
            EmitChangeEvent();
        }

        public void ResumeCalendar()
        {
            IsCalendarPaused = false;
            EmitChangeEvent();
        }

        public void PausePhysics()
        {
            IsPhysicsPaused = true;
            EmitChangeEvent();
        }

        public void ResumePhysics()
        {
            IsPhysicsPaused = false;
            EmitChangeEvent();
        }

        public void PauseTimeflow()
        {
            IsAllTimeflowPaused = true;
            EmitChangeEvent();
        }

        public void ResumeTimeflow()
        {
            IsAllTimeflowPaused = false;
            EmitChangeEvent();
        }

        public void SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            RecalculateEffectiveCalendarTimeScale();
            EmitChangeEvent();
            _logger.LogCategory("Time", $"Set time scale to {timeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        public void SetCalendarTimeScale(float timeScale)
        {
            CalendarTimeScale = timeScale;
            RecalculateEffectiveCalendarTimeScale();
            EmitChangeEvent();
            _logger.LogCategory("Time", $"Set calendar time scale to {timeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        public void ResetTimeScale()
        {
            TimeScale = _timeSettings.DefaultTimeScale;
            RecalculateEffectiveCalendarTimeScale();
            EmitChangeEvent();
            _logger.LogCategory("Time", $"Reset time scale to {TimeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        public void ResetCalendarScale()
        {
            CalendarTimeScale = _timeSettings.DefaultCalendarTimeScale;
            RecalculateEffectiveCalendarTimeScale();
            EmitChangeEvent();
            _logger.LogCategory("Time", $"Reset calendar time scale to {CalendarTimeScale} (ECT={EffectiveCalendarTimeScale})");
        }

        private void RecalculateEffectiveCalendarTimeScale()
            => EffectiveCalendarTimeScale = CalendarTimeScale * TimeScale;

        private void EmitChangeEvent()
        {
            _timeChangeSubject.Emit(new TimeChangeEvent(
                realTimeScale: TimeScale,
                calendarTimeScale: EffectiveCalendarTimeScale,
                isRealTimePaused: IsRealTimePaused,
                isCalendarPaused: IsCalendarPaused)); ; // TODO: physics paused?
        }
    }
}
