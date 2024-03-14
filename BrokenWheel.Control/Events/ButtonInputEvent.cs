using System;
using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Events
{
    public partial class ButtonInputEvent
    {
        /// <summary>
        /// The abstracted input.
        /// </summary>
        public RPGInput Input { get; }

        /// <summary>
        /// Whether the input is of a custom type.
        /// </summary>
        public bool IsCustomInput { get; }

        /// <summary>
        /// A custom input not included in the base system.
        /// </summary>
        public string CustomInput { get; }

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

        public ButtonInputEvent(RPGInput input, PressType pressType, double deltaTime, double heldTime)
        {
            Input = input;
            IsCustomInput = input == RPGInput.Custom;
            CustomInput = "";
            PressType = pressType;
            DeltaTime = deltaTime;
            HeldTime = heldTime;
        }

        public ButtonInputEvent(string input, PressType pressType, double deltaTime, double heldTime)
        {
            Input = RPGInput.Custom;
            IsCustomInput = true;
            CustomInput = input ?? throw new ArgumentNullException(nameof(input));
            PressType = pressType;
            DeltaTime = deltaTime;
            HeldTime = heldTime;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var pressData = $"{PressType}|{HeldTime}s|d{DeltaTime}";
            return IsCustomInput
                ? $"BI[Custom({CustomInput}):{pressData}]"
                : $"BI[{Input}:{pressData}]";
        }
    }
}
