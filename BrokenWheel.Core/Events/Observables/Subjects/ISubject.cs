namespace BrokenWheel.Core.Events.Observables.Subjects
{
    public interface ISubject<TEvent> : IObservable<TEvent> where TEvent : GameEvent
    {
        /// <summary>
        /// Gets the subject as an observable with no emit capability.
        /// </summary>
        IObservable<TEvent> AsObservable();

        /// <summary>
        /// Update value and emit a <see cref="GameEvent"/> to all 
        /// </summary>
        void Emit(TEvent @event);
    }
}
