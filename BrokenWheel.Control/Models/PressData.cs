using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Control.Models
{
    /// <summary>
    /// When a button is pressed, different actions might be performed based on context.
    /// For example, holding left click to aim a rifle and releasing to fire, versus
    /// tapping to hip-fire.
    /// </summary>
	public readonly struct PressData
    {
        /// <summary>
        /// Time passed in seconds since the last event happened.
        /// </summary>
        public double DeltaTime { get; }

        public bool IsAltPress { get; }

        /// <summary>
        /// The <see cref="PressType"/> of the input.
        /// </summary>
        public PressType Type { get; }


        public PressData(double deltaTime, bool isAltPress, bool isPressed, bool isHeld)
        {
            DeltaTime = deltaTime;
            IsAltPress = isAltPress;
            Type = Util.Util.GetButtonPressTypeFromBooleans(isPressed, isHeld);
        }

        public PressData(double deltaTime, PressType pressType, bool isAltPress = false)
        {
            DeltaTime = deltaTime;
            IsAltPress = isAltPress;
            Type = pressType;
        }
    }
}
