namespace BrokenWheel.Control.Models.InputData
{
    public readonly struct MoveInputData
    {
        public const string CATEGORY = "Movement";

        /// <summary>
        /// The amount of time since the last tick.
        /// </summary>
        public double DeltaTime { get; }

        /// <summary>
        /// Total time the button has been held, from press to release.
        /// Does not reset until the tick after release.
        /// </summary>
        public double HeldTime { get; }

        /// <summary>
        /// The X velocity of the analog input, between -1.0 and +1.0.
        /// </summary>
        public double VelocityX { get; }

        /// <summary>
        /// The Y velocity of the analog input, between -1.0 and +1.0.
        /// </summary>
        public double VelocityY { get; }

        public MoveInputData(double delta, double heldTime, double vX, double vY)
        {
            DeltaTime = delta;
            HeldTime = heldTime;
            VelocityX = vX;
            VelocityY = vY;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"I[Move-d{DeltaTime}h{HeldTime}-v{VelocityX},{VelocityY}]";
        }
    }
}
