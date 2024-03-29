﻿namespace BrokenWheel.Core.Events
{
    public interface IEventSubject<TEvent> : IEventObservable<TEvent>
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
