using LorendisCore.Player.Control;

namespace LorendisCore.Utilities
{
    public static class Util
    {
        public static ButtonPressType GetButtonPressTypeFromBooleans(bool isPressed, bool isHeld)
        {
            if (isPressed)
                return isHeld
                    ? ButtonPressType.Held
                    : ButtonPressType.Pressed;
            else
                return isHeld
                    ? ButtonPressType.Released
                    : ButtonPressType.NotHeld;
        }

        public static bool ButtonPressTypeWasJustPressed(ButtonPressType type)
        {
            return type == ButtonPressType.Pressed 
                || type == ButtonPressType.Held;
        }

        public static bool ButtonPressTypeWasHeldLast(ButtonPressType type)
        {
            return type == ButtonPressType.Held
                || type == ButtonPressType.Released;
        }
    }
}
