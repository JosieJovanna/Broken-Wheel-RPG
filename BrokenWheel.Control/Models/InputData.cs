using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Models
{
    /// <summary>
    /// A struct tracking a single input's state.
    /// Actively changed in the <see cref="IRPGInputHandler"/> but unaffected by downstream changes.
    /// </summary>
    public struct InputData
    {
        /// <summary>
        /// The abstracted input.
        /// </summary>
        public RPGInput Input { get; } // TODO: add support for custom input

        /// <summary>
        /// Whether just pressed, just released, held, or not held.
        /// </summary>
        public PressType PressType { set; get; }

        /// <summary>
        /// The amount of time since the last tick.
        /// </summary>
        public double DeltaTime { set; get; }

        /// <summary>
        /// Total time the button has been held, from press to release.
        /// Does not reset until the tick after release.
        /// </summary>
        public double HeldTime { set; get; }

        public override string ToString()
        {
            return $"I[{Input}: {PressType} - d{DeltaTime} h{HeldTime}]";
        }
    }
}
