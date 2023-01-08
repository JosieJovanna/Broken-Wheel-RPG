namespace LorendisCore.Player.Control
{
    public enum ButtonPressType
    {
        /// <summary>
        /// Not pressed and not held last frame.
        /// </summary>
        NotHeld,

        /// <summary>
        /// Pressed but not held last frame.
        /// </summary>
        Pressed,

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
