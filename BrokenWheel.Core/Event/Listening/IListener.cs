namespace BrokenWheel.Core.Event.Listening
{
    public interface IListener<T> where T : GameEvent
    {
        void HandleEvent(T gameEvent);
    }
}
