namespace BrokenWheel.Control.Models.InputData
{
    public readonly struct CursorInputData
    {
        /// <summary>
        /// X coordinate relative to the centre of the screen.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y coordinate relative to the centre of the screen.
        /// </summary>
        public int Y { get; }

        public CursorInputData(int x, int y)
        {
            (X, Y) = (x, y);
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            return $"CI[Cursor:{X},{Y}]";
        }
    }
}
