using BrokenWheel.Core.Events.Abstract;

namespace BrokenWheel.Core.Events.Handling
{
    public interface IEventHandler<in TEvent> where TEvent : GameEvent
    {
        void HandleEvent(TEvent gameEvent);
    }
}
