using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Models
{
    public class InputData
    {
        /// <summary>
        /// The abstracted input.
        /// </summary>
        public GameInput Input { get; }

        /// <summary>
        /// Whether just pressed, just released, held, or not held.
        /// </summary>
        public PressType PressType { get; }

        /// <summary>
        /// The amount of time since the last tick.
        /// </summary>
        public double DeltaTime { get; }

        /// <summary>
        /// Total time the button has been held, from press to release.
        /// Does not reset until the tick after release.
        /// </summary>
        public double HeldTime { get; }

        public InputData(GameInput input, PressType pressType, double deltaTime, double heldTime)
        {
            Input = input;
            PressType = pressType;
            DeltaTime = deltaTime;

        }
    }
}
