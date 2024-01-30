namespace BrokenWheel.Core.Events
{
    public delegate void EventHandler<in T>(T gameEvent) where T : GameEvent;
}
