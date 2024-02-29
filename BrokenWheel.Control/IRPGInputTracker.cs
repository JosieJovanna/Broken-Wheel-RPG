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
        void TrackButtonInput(RPGInput input, bool isPressed);

        /// <summary>
        /// Caches current movement input state for processing
        /// </summary>
        void TrackMoveInput(double vX, double vY);

        /// <summary>
        /// Caches current look input state for processing.
        /// </summary>
        void TrackCursorLookInput(double vX, double vY, int posX, int posY);

        /// <summary>
        /// Processes input based on current state.
        /// </summary>
        /// <param name="delta"> Time passed since last call. </param>
        void ProcessInputs(double delta);
    }
}
