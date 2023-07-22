namespace BrokenWheel.Core.Event.Listening
{
    public interface IEntityEventListener : IEventListener
    {
        /// <summary>
        /// The ID of the entity that these events originate from. TODO: Make equivalent of this class for all events
        /// </summary>
        string EntityId { get; }
    }
}
