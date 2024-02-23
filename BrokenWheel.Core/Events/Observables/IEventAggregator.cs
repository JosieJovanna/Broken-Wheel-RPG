using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables
{
    public interface IEventAggregator
    {
        IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent;

        IEventSubject<TEvent> GetSubject<TEvent>()
            where TEvent : GameEvent;

        void Subscribe<TEvent>(EventHandlerFunction<TEvent> function)
            where TEvent : GameEvent;
    }
}
