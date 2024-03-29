﻿namespace BrokenWheel.Control.Enum
{
    /// <summary>
    /// A button press's input state.
    /// </summary>
    public enum PressType
    {
        /// <summary>
        /// Not pressed and not held last frame.
        /// </summary>
        NotHeld = default,

        /// <summary>
        /// Pressed but not held last frame.
        /// </summary>
        Clicked,

        /// <summary>
        /// Pressed and held last frame.
        /// </summary>
        Held,

        /// <summary>
        /// Not pressed but held last frame.
        /// </summary>
        Released
    }
}
