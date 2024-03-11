namespace BrokenWheel.Core.Time.Listeners
{
    /// <summary>
    /// Has a function that is called every tick given in-game real time.
    /// Automatically hooks into the time ticker.
    /// </summary>
    public interface IOnRealTime
    {
        void OnRealTime(double delta);
    }
}
