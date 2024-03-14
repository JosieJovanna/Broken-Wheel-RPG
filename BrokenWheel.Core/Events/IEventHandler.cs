namespace BrokenWheel.Core.Events
{
    /// <summary>
    /// Any object that automatically listens to events of the given type.
    /// </summary>
    public interface IEventHandler<in TEvent>
    {
        void HandleEvent(TEvent gameEvent);
    }
}
