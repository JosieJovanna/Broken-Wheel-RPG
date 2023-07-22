namespace BrokenWheel.Core.Event
{
    public delegate void EventHandler<in T>(T gameEvent) where T : GameEvent;
}
