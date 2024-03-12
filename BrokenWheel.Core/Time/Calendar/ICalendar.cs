using BrokenWheel.Core.Time.Listeners;

namespace BrokenWheel.Core.Time.Calendar
{
    /// <summary>
    /// The object which keeps track of the current in-game calendar date/time.
    /// </summary>
    public interface ICalendar : IOnCalendarTime
    {
        void SetDateTime(GameDateTime dateTime);

        GameDateTime Now();

        void Pause();
        void Resume();
    }
}
