namespace BrokenWheel.Core.Time.Listeners
{
    /// <summary>
    /// Has a function that is called every tick given scaled calendar time.
    /// Automatically hooks into the time ticker.
    /// </summary>
    public interface IOnCalendarTime
    {
        void OnCalendarTime(double delta);
    }
}
