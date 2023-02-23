﻿using LorendisCore.Player.Control;

namespace LorendisCore.Utilities
{
    public static class Util
    {
        public static PressType GetButtonPressTypeFromBooleans(bool isPressed, bool isHeld)
        {
            if (isPressed)
                return isHeld
                    ? PressType.Held
                    : PressType.Clicked;
            else
                return isHeld
                    ? PressType.Released
                    : PressType.NotHeld;
        }

        public static bool ButtonPressTypeWasJustPressed(PressType type)
        {
            return type == PressType.Clicked 
                || type == PressType.Held;
        }

        public static bool ButtonPressTypeWasHeldLast(PressType type)
        {
            return type == PressType.Held
                || type == PressType.Released;
        }
    }
}
