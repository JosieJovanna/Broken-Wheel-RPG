using BrokenWheel.Core.Time.Enum;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Time.Calendar
{
    public readonly struct GameDateTime
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int Week { get; }
        public int DayOfWeek { get; }
        public Weekday GetWeekday { get => (Weekday)DayOfWeek; }
        public Month GetMonth { get => (Month)Month; }

        public GameDateTime(int year, int month, int day, int hour = 1, int minute = 1, int second = 1)
        {
            Year = year;

#pragma warning disable format
            Month  =  month.ValidatedInRange(1, TimeConstants.NUM_MONTHS,      nameof(month));
            Day    =    day.ValidatedInRange(1, TimeConstants.NUM_DAYS,        nameof(day));
            Hour   =   hour.ValidatedInRange(0, TimeConstants.NUM_HOURS - 1,   nameof(hour));
            Minute = minute.ValidatedInRange(0, TimeConstants.NUM_MINUTES - 1, nameof(hour));
            Second = second.ValidatedInRange(0, TimeConstants.NUM_SECONDS - 1, nameof(second));
            
            Week      = (Day / TimeConstants.NUM_DAYS_PER_WEEK) + 1;
            DayOfWeek = (Day % TimeConstants.NUM_DAYS_PER_WEEK) + 1;
#pragma warning restore format
        }

        // TODO: moon phase?

        // TODO: math overrides
    }
}
