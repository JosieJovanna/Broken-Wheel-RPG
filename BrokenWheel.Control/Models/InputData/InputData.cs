using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Models.InputData
{
    public readonly struct InputData
    {
        /// <summary>
        /// The abstracted input.
        /// </summary>
        public RPGInput Input { get; } // TODO: add support for custom input

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

        public InputData(RPGInput input, PressType pressType, double deltaTime, double heldTime)
        {
            Input = input;
            PressType = pressType;
            DeltaTime = deltaTime;
            HeldTime = heldTime;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"I[{Input}: {PressType}-d{DeltaTime}h{HeldTime}]";
        }
    }
}
