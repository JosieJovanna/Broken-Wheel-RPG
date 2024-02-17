using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control
{
    /// <summary>
    /// Takes input events, does some logic, and uses them to trigger appropriate actions.
    /// </summary>
    public interface IRPGInputTracker
    {
        /// <summary>
        /// Caches current digital input state for processing.
        /// </summary>
        /// <param name="data"> RPG input data. </param>
        void HandleButtonInput(RPGInput input, bool isPressed);

        /// <summary>
        /// Handles move input per tick.
        /// </summary>
        void HandleMoveInput(MovementInputData moveInput);

        /// <summary>
        /// Handles look input per tick.
        /// </summary>
        void HandleLookInput(LookInputData lookInput);

        /// <summary>
        /// Processes input based on current state.
        /// </summary>
        /// <param name="delta"> Time passed since last call. </param>
        void ProcessInputs(double delta);

    }
}
