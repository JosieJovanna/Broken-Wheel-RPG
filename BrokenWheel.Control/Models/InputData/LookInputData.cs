namespace BrokenWheel.Control.Models.InputData
{
    public readonly struct LookInputData
    {
        /// <summary>
        /// The amount of time since the last tick.
        /// </summary>
        public double DeltaTime { get; }

        /// <summary>
        /// Total time the button was held.
        /// Does not reset until the tick after release.
        /// </summary>
        public double HeldTime { get; }

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
        public int PositionX { get; }

        /// <summary>
        /// The Y position of the cursor, relative to the centre of the screen.
        /// </summary>
        public int PositionY { get; }

        public LookInputData(double delta, double held, double vX, double vY, int centreX, int centreY)
        {
            DeltaTime = delta;
            HeldTime = held;
            VelocityX = vX;
            VelocityY = vY;
            PositionX = centreX;
            PositionY = centreY;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"I[Move-d{DeltaTime}h{HeldTime}-v{VelocityX},{VelocityY}-@{PositionX},{PositionY}]";
        }
    }
}
