namespace BrokenWheel.Core.Time
{
    /// <summary>
    /// A version of the <see cref="ITimeService"/> which also allows for the changing of the flow of time.
    /// </summary>
    public interface ITimeScaler
    {
        /// <summary>
        /// Sets the scale of time relative to IRL tick time.
        /// </summary>
        void SetTimeScale(float timeScale);


        /// <summary>
        /// Sets the scale of calendar time, relative to IRL tick time.
        /// </summary>
        void SetCalendarTimeScale(float timeScale);

        /// <summary>
        /// Resets the scale of real time to be equal IRL tick time.
        /// </summary>
        void ResetTimeScale();

        /// <summary>
        /// Resets the calendar time scale, in the case that it was modified, for, say a trip to another dimension in which time scales differently.
        /// </summary>
        void ResetCalendarScale();

        /// <summary>
        /// Pauses all realtime-based processing, including movement, physics, and any non-UI based gameplay.
        /// </summary>
        void PauseRealTime();

        /// <summary>
        /// Resumes all realtime-based processing.
        /// </summary>
        void ResumeRealTime();

        /// <summary>
        /// Pauses the in-game calendar.
        /// </summary>
        void PauseCalendar();

        /// <summary>
        /// Resumes the in-game calendar.
        /// </summary>
        void ResumeCalendar();

        /// <summary>
        /// Pauses physics timers.
        /// </summary>
        void PausePhysics();

        /// <summary>
        /// Resumes physics timers.
        /// </summary>
        void ResumePhysics();
    }
}
