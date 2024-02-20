﻿namespace BrokenWheel.Core.Events.Observables
{
    public interface IEventSubject<TEvent> : IEventObservable<TEvent> where TEvent : GameEvent
    {
        /// <summary>
        /// Gets the subject as an observable with no emit capability.
        /// </summary>
        IEventObservable<TEvent> AsObservable();

        /// <summary>
        /// Update value and emit a <see cref="GameEvent"/> to all 
        /// </summary>
        void Emit(TEvent @event);
    }
}