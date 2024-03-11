using BrokenWheel.Core.Time.Enum;

namespace BrokenWheel.Core.Time.GameDate
{
    public partial class GameDate
    {
        private int _year;
        private int _month;
        private int _day;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private double _second;

        /// <summary>
        /// Current in-game year.
        /// </summary>
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
            }
        }

        /// <summary>
        /// Current in-game month, from 1 to 6 inclusive.
        /// </summary>
        public int Month
        {
            get => _month;
            set
            {
                _month = value % TimeConstants.NUM_MONTHS;
                _year += value / TimeConstants.NUM_MONTHS;
            }
        }

        /// <summary>
        /// Current in-game day of the month, from 1 to 60 inclusive.
        /// </summary>
        public int Day
        {
            get => _day;
            set
            {
                _day = value % TimeConstants.NUM_DAYS;
                _month += value / TimeConstants.NUM_DAYS;
            }
        }

        /// <summary>
        /// Current hour in the day, with 0 at midnight, and 23 as 11pm.
        /// </summary>
        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value % TimeConstants.NUM_HOURS;
                _day += value / TimeConstants.NUM_HOURS;
            }
        }

        /// <summary>
        /// Current minute in the hour, from 0 to 59 inclusive.
        /// </summary>
        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value % TimeConstants.NUM_MINUTES;
                _hours += value / TimeConstants.NUM_MINUTES;
            }
        }

        /// <summary>
        /// Current second in the minute, from 0 to 59 inclusive.
        /// </summary>
        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value % TimeConstants.NUM_SECONDS;
                _minutes += value / TimeConstants.NUM_SECONDS;
            }
        }

        /// <summary>
        /// Current decimal towards the next second, from 0 inclusive to 1 exclusive.
        /// </summary>
        public double Second
        {
            get => _second;
            set
            {
                _second = value % 1;
                _seconds += (int)value;
            }
        }

        public Month MonthEnum => (Month)Month;

        public Weekday DayOfWeek => (Weekday)(Day % TimeConstants.NUM_DAYS_PER_WEEK);

        public GameDate(int year, int month = 1, int day = 1, int hours = 0, int minutes = 0, int seconds = 0, double second = 0)
        {
            Year = year;
            Month = month;
            Day = day;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Second = second;
        }

        private void CalculateOverflow()
        {

        }
    }
}
