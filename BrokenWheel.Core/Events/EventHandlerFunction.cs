namespace BrokenWheel.Core.Events
{
    public delegate void EventHandlerFunction<in T>(T gameEvent);
}
