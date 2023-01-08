using LorendisCore.Utilities;

namespace LorendisCore.Player.Control
{
    /// <summary>
    /// When a button is pressed, different actions might be performed based on context.
    /// For example, holding left click to aim a rifle and releasing to fire, versus
    /// tapping to hip-fire.
    /// </summary>
	public struct ButtonPressData
	{
		public ButtonPressData(double deltaTime, bool isPressed, bool isHeld)
		{
			DeltaTime = deltaTime;
			IsPressed = isPressed;
			IsHeld = isHeld;
		}

        public ButtonPressData(double deltaTime, ButtonPressType buttonPressType)
        {
            DeltaTime = deltaTime;
            IsPressed = Util.ButtonPressTypeWasJustPressed(buttonPressType);
            IsHeld = Util.ButtonPressTypeWasHeldLast(buttonPressType);
        }

		/**
		 * Time passed in seconds since the last event happened.
		 */
		public double DeltaTime { get; }

		/**
		 * Whether the key is pressed.
		 * Will be true if the button was just pressed or is being held.
		 * Will be false if the button was released, or is not held.
		 */
		public bool IsPressed { get; }

		/**
		 * Whether the key is being held.
		 * Will be true if the button is being held, or if it was just released after being held.
		 * Will be false if the button was just pressed, or is not held.
		 */
		public bool IsHeld { get; }

        public ButtonPressType GetButtonPressType()
        {
            return Util.GetButtonPressTypeFromBooleans(IsPressed, IsHeld);
        }
	}
}
