namespace BrokenWheel.Core.Events
{
    public delegate void EventHandler<in T>(T data) where T : GameEvent;
}
