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

        public bool IsStopped { get; }

        public LookInputData(double delta, double held, double vX, double vY)
        {
            DeltaTime = delta;
            HeldTime = held;
            VelocityX = vX;
            VelocityY = vY;
            IsStopped = vX == 0.0 && vY == 0.0;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"LI[Look:{HeldTime}s|v({VelocityX},{VelocityY})|d{DeltaTime}]";
        }
    }
}
