namespace BrokenWheel.Core.Events.Observables.Subjects
{
    public interface ICategorizedSubject<TEvent>
        : IEventSubject<TEvent>, ICategorizedObservable<TEvent>
        where TEvent : CategorizedEvent
    {
        /// <summary>
        /// Returns the subject as a readonly <see cref="ICategorizedObservable{TEvent}"/>.
        /// </summary>
        ICategorizedObservable<TEvent> AsCategorizedObservable();
    }
}
