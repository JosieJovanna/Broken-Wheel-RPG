namespace BrokenWheel.Core.Events.Listening
{
    public interface IListener<T> where T : GameEvent
    {
        void HandleEvent(T gameEvent);
    }
}
