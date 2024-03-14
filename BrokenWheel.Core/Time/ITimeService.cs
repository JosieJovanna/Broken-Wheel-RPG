namespace BrokenWheel.Core.Time
{
    /// <summary>
    /// The service used to register all time-based programming above the engine-level implementation.
    /// Allows services to listen to time changes without a chain of calling tick methods.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// The amount by which all time is multiplied.
        /// </summary>
        float TimeScale { get; }

        /// <summary>
        /// The amount of in-game calendar seconds equivalent to one realtime second.
        /// </summary>
        float CalendarTimeScale { get; }

        /// <summary>
        /// The amount of calendar seconds equivalent to one unmodified IRL second.
        /// </summary>
        float EffectiveCalendarTimeScale { get; }


        /// <summary>
        /// Whether real time is currently flowing.
        /// </summary>
        bool IsRealTimePaused { get; }

        /// <summary>
        /// Whether physics are processing. (Likely true if time is reversing.)
        /// </summary>
        bool IsPhysicsPaused { get; }

        /// <summary>
        /// Whether calendar time is currently flowing.
        /// </summary>
        bool IsCalendarPaused { get; }

        /// <summary>
        /// Whether all time flow is paused, independent of other Is-X-Paused booleans.
        /// If true, no time will flow, no matter if individual time scales are paused.
        /// </summary>
        bool IsAllTimeflowPaused { get; }


        /// <summary>
        /// Adds a function to be called every game tick, which passes IRL tick delta time.
        /// </summary>
        void AddTickTimeFx(TimeFunction timeFx);

        /// <summary>
        /// Removes a function which expects IRL tick delta time.
        /// </summary>
        void RemoveTickTimeFx(TimeFunction timeFx);


        /// <summary>
        /// Adds a function to be called every game tick, which passes real delta time (time slowing effects, etc).
        /// </summary>
        void AddRealTimeFx(TimeFunction timeFx);

        /// <summary>
        /// Removes a function which expects real delta time (time slowing effects, etc).
        /// </summary>
        void RemoveRealTimeFx(TimeFunction realTimeFx);


        /// <summary>
        /// Adds a function to be called every game tick, which passes the canonical, scaled, calendar delta time.
        /// </summary>
        void AddCalendarTimeFx(TimeFunction timeFx);

        /// <summary>
        /// Removes a function which expects calendar delta time.
        /// </summary>
        void RemoveCalendarTimeFx(TimeFunction timeFx);
    }
}
