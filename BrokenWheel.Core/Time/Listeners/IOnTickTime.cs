namespace BrokenWheel.Core.Time.Listeners
{
    /// <summary>
    /// Has a function that is called every tick given IRL tick time.
    /// Automatically hooks into the time ticker.
    /// </summary>
    public interface IOnTickTime
    {
        void OnTickTime(double delta);
    }
}
