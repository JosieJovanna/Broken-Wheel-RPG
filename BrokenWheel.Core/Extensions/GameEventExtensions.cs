using BrokenWheel.Core.Events;

namespace BrokenWheel.Core.Extensions
{
    public static class GameEventExtensions
    {
        public static bool IsEnumSwitch(this GameEvent @event)
        {

        }

        public static bool IsStringEnumSwitch(this GameEvent @event)
        {
            return @event.InheritsEvent<StringEnumSwitchGameEvent<>>
        }

        public static bool InheritsEvent<TEvent>(this GameEvent @event) where TEvent : GameEvent
        {
            var actualType = @event.GetType();
            var expectedType = typeof(TEvent);

            return actualType.IsAssignableFrom(expectedType);
        }
    }
}
