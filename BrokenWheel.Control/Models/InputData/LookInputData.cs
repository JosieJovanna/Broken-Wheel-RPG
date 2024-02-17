namespace BrokenWheel.Control.Models.InputData
{
    public readonly struct LookInputData
    {
        /// <summary>
        /// The amount of time since the last tick.
        /// </summary>
        public double DeltaTime { get; }

        /// <summary>
        /// The X velocity of the analog input.
        /// </summary>
        public double VelocityX { get; }

        /// <summary>
        /// The Y velocity of the analog input.
        /// </summary>
        public double VelocityY { get; }

        /// <summary>
        /// The X position of the cursor, relative to the centre of the screen.
        /// </summary>
        public int CentreX { get; }

        /// <summary>
        /// The Y position of the cursor, relative to the centre of the screen.
        /// </summary>
        public int CentreY { get; }

        /// <summary>
        /// The X position relative to the top-left corner of the window.
        /// </summary>
        public int CornerX { get; }

        /// <summary>
        /// The Y position relative to the top-left corner of the window.
        /// </summary>
        public int CornerY { get; }
    }
}
