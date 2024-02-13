using BrokenWheel.Control.Models;

namespace BrokenWheel.Control
{
    /// <summary>
    /// Takes input events, does some logic, and uses them to trigger appropriate actions.
    /// </summary>
    public interface IRPGInputHandler
    {
        /// <summary>
        /// Caches current input state for processing.
        /// </summary>
        /// <param name="data"> RPG input data. </param>
        void HandleInput(InputData data);

        /// <summary>
        /// Processes input based on current state.
        /// </summary>
        /// <param name="delta"> Time passed since last call. </param>
        void Process(double delta);
    }
}
