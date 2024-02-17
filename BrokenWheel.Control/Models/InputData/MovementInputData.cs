namespace BrokenWheel.Control.Models.InputData
{
    public readonly struct MovementInputData
    {
        /// <summary>
        /// The amount of time since the last tick.
        /// </summary>
        public double DeltaTime { get; }

        /// <summary>
        /// The X velocity of the analog input, between -1.0 and +1.0.
        /// </summary>
        public double VelocityX { get; }

        /// <summary>
        /// The Y velocity of the analog input, between -1.0 and +1.0.
        /// </summary>
        public double VelocityY { get; }

        public MovementInputData(double delta, double vX, double vY)
        {
            DeltaTime = delta;
            VelocityX = vX;
            VelocityY = vY;
        }
    }
}
