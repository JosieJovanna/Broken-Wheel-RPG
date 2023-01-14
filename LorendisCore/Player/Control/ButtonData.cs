using LorendisCore.Utilities;

namespace LorendisCore.Player.Control
{
    /// <summary>
    /// When a button is pressed, different actions might be performed based on context.
    /// For example, holding left click to aim a rifle and releasing to fire, versus
    /// tapping to hip-fire.
    /// </summary>
	public readonly struct ButtonData
	{
		/// <summary>
		/// Time passed in seconds since the last event happened.
		/// </summary>
		public double DeltaTime { get; }

		/// <summary>
		/// The type of event. More useful than getting booleans and 
		/// </summary>
		public ButtonPressType Type { get; }


		public ButtonData(double deltaTime, bool isPressed, bool isHeld)
        {
            DeltaTime = deltaTime;
            Type = Util.GetButtonPressTypeFromBooleans(isPressed, isHeld);
        }

        public ButtonData(double deltaTime, ButtonPressType buttonPressType)
        {
            DeltaTime = deltaTime;
            Type = buttonPressType;
        }
	}
}
