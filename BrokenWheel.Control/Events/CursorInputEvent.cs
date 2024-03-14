namespace BrokenWheel.Control.Events
{
    public partial class CursorInputEvent
    {
        /// <summary>
        /// X coordinate relative to the centre of the screen.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y coordinate relative to the centre of the screen.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// X coordinate relative to the centre of the screen, in UI's scale.
        /// </summary>
        public int ScaledX { get; }

        /// <summary>
        /// XY coordinate relative to the centre of the screen, in UI's scale.
        /// </summary>
        public int ScaledY { get; }

        public CursorInputEvent(int x, int y, int scale)
        {
            (X, Y) = (x, y);
            (ScaledX, ScaledY) = (x / scale, y / scale);
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            return $"CI[Cursor:{X},{Y}({ScaledX},{ScaledY})]";
        }
    }
}
