using System;

namespace BrokenWheel.Core.Events.Implementation
{
    public partial class EventSubject<TEvent>
    {
        private sealed class ConditionalHandler
        {
            public int ID { get; }
            public Func<TEvent, bool> Predicate { get; }
            public EventHandlerFunction<TEvent> Handler { get; set; }

            public ConditionalHandler(Func<TEvent, bool> predicate, EventHandlerFunction<TEvent> handler, int id)
            {
                ID = id;
                Predicate = predicate;
                Handler = handler;
            }
        }
    }
}
