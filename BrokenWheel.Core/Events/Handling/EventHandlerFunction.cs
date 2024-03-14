namespace BrokenWheel.Core.Events.Handling
{
    public delegate void EventHandlerFunction<in T>(T gameEvent);
}
