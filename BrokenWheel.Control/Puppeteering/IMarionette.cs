using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Puppeteering
{
    /// <summary>
    /// A <see cref="IPuppet"/> which also handles actions.
    /// </summary>
    public interface IMarionette : IPuppet
    {
        void Jump(float strength);

        void SetMovementState(MovementStance stance, MovementSpeed speed);

        // TODO: action handling
    }
}
