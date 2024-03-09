using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Puppeteering
{
    /// <summary>
    /// A <see cref="IPuppet"/> which also handles actions.
    /// </summary>
    public interface IMarionette : IPuppet
    {
        /// <summary>
        /// Whether this entity is on the ground (beneath it, relatively).
        /// </summary>
        bool IsOnGround { get; }

        /// <summary>
        /// How long, in seconds, this entity has been in the air.
        /// </summary>
        double TimeInAir { get; }

        /// <summary>
        /// The number of jumps performed by this entity since last touching the ground.
        /// </summary>
        int NumberOfJumps { get; }

        /// <summary>
        /// The current effective height of the marionette.
        /// </summary>
        float Height { get; }

        /// <summary>
        /// Current speed setting of the marionette.
        /// </summary>
        MovementSpeed MovementSpeed { get; }

        /// <summary>
        /// Current stance setting of the marionette.
        /// </summary>
        MovementStance MovementStance { get; }

        /// <summary>
        /// Sets the modes of movement for this entity.
        /// </summary>
        void SetMovementState(MovementStance stance, MovementSpeed speed);

        /// <summary>
        /// Make this entity jump, with the specified strength.
        /// </summary>
        void Jump(float strength);

        // TODO: action handling
    }
}
