using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Events.Observables.Subjects;

namespace BrokenWheel.Core.Events.Aggregation
{
    public interface IEventAggregator
    {
        IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent;

        IEventSubject<TEvent> GetSubject<TEvent>()
            where TEvent : GameEvent;

        ICategorizedObservable<TEvent> GetCategorizedObservable<TEvent>()
            where TEvent : CategorizedEvent;

        ICategorizedSubject<TEvent> GetCategorizedSubject<TEvent>()
            where TEvent : CategorizedEvent;
    }
}
