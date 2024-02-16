using BrokenWheel.Control.Enum;

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
        /// Caches current analog input state for processing.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"> Horizontal strength of the analog input, from -1.0 to +1.0. </param>
        /// <param name="y"> Vertical strength of the analog input, from -1.0 to +1.0. </param>
        void HandleAnalogInput(RPGInput input, double x, double y);

        /// <summary>
        /// Processes input based on current state.
        /// </summary>
        /// <param name="delta"> Time passed since last call. </param>
        void ProcessInputs(double delta);
    }
}
