namespace BrokenWheel.Core.Events.Listening
{
    public interface IEntityEventNexus : IEventNexus
    {
        /// <summary>
        /// The ID of the entity that these events originate from. TODO: Make equivalent of this class for all events
        /// </summary>
        string EntityId { get; }
    }
}
