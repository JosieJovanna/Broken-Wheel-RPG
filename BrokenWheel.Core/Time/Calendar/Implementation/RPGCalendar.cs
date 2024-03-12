using System;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Time.Enum;

namespace BrokenWheel.Core.Time.Calendar.Implementation
{
    public partial class RPGCalendar
        : ICalendar
        , IEventHandler<GameModeUpdateEvent>
    {
        private readonly ILogger _logger;

        private bool _isPaused = false;

        private int _year;
        private int _month;
        private int _day;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private double _second;

        protected int Year
        {
            get => _year;
            set
            {
                _year = value;
            }
        }

        protected int Months
        {
            get => _month;
            set
            {
                _month = value % TimeConstants.NUM_MONTHS;
                Year += value / TimeConstants.NUM_MONTHS;
            }
        }

        protected int Days
        {
            get => _day;
            set
            {
                _day = value % TimeConstants.NUM_DAYS;
                Months += value / TimeConstants.NUM_DAYS;
            }
        }

        protected int Hours
        {
            get => _hours;
            set
            {
                _hours = value % TimeConstants.NUM_HOURS;
                Days += value / TimeConstants.NUM_HOURS;
            }
        }

        protected int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value % TimeConstants.NUM_MINUTES;
                Hours += value / TimeConstants.NUM_MINUTES;
            }
        }

        protected int Seconds
        {
            get => _seconds;
            set
            {
                // TODO: negative values...
                _seconds = value % TimeConstants.NUM_SECONDS;
                Minutes += value / TimeConstants.NUM_SECONDS;
            }
        }

        protected double Second
        {
            get => _second;
            set
            {
                _second = value % 1;
                Seconds += (int)value;
            }
        }

        public RPGCalendar(ILogger logger, GameDateTime? initialDateTime = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (initialDateTime != null)
                SetDateTime((GameDateTime)initialDateTime);
            // TODO: default start datetime
        }

        public void HandleEvent(GameModeUpdateEvent gameEvent)
        {
            if (gameEvent.GameMode == GameMode.Gameplay)
                Resume();
            else
                Pause();
        }

        public void Pause()
        {
            _isPaused = true;
            _logger.Log("Calendar paused");
        }

        public void Resume()
        {
            _isPaused = false;
            _logger.Log("Calendar resumed");
        }

        public void OnCalendarTime(double delta)
        {
            if (!_isPaused)
                Second += delta;
        }

        public void SetDateTime(GameDateTime dateTime)
        {
#pragma warning disable format
            Year    = dateTime.Year;
            Months  = dateTime.Month - 1;
            Days    = dateTime.Day - 1;
            Hours   = dateTime.Hour;
            Minutes = dateTime.Minute;
            Seconds = dateTime.Second;
            Second  = 0;
#pragma warning restore format
        }

        public GameDateTime Now()
        {
#pragma warning disable format
            return new GameDateTime(
                year   : Year,
                month  : Months + 1,
                day    : Days + 1,
                hour   : Hours,
                minute : Minutes,
                second : Seconds);
#pragma warning restore format
        }
    }
}
