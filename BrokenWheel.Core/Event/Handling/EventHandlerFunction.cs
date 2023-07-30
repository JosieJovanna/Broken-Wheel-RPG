namespace BrokenWheel.Core.Event.Handling
{
    public delegate void EventHandlerFunction<in T>(T gameEvent) where T : GameEvent;
}
