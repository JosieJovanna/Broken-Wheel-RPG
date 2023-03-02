using LorendisCore.Utilities;

namespace LorendisCore.Control.Models
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

		/// <summary>
		/// The <see cref="PressType"/> of the input.
		/// </summary>
		public PressType Type { get; }


		public PressData(double deltaTime, bool isPressed, bool isHeld)
        {
            DeltaTime = deltaTime;
            Type = Util.GetButtonPressTypeFromBooleans(isPressed, isHeld);
        }

        public PressData(double deltaTime, PressType pressType)
        {
            DeltaTime = deltaTime;
            Type = pressType;
        }
	}
}
