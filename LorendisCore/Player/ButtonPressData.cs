namespace LorendisCore.Player
{
  public class ButtonPressData
  {
    public ButtonPressData(double deltaTime, bool isPressed, bool isHeld)
    {
      DeltaTime = deltaTime;
      IsPressed = isPressed;
      IsHeld = isHeld;
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
  }
}
