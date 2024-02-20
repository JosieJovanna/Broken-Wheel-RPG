namespace BrokenWheel.Core.Events.Observables
{
    public interface IEventAggregator
    {
        IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent;

        IEventSubject<TEvent> GetSubject<TEvent>()
            where TEvent : GameEvent;
    }
}
