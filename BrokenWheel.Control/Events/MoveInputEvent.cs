namespace BrokenWheel.Control.Events
{
    public partial class MoveInputEvent
    {
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

        /// <summary>
        /// Whether velocity is 0.
        /// </summary>
        public bool IsStopped { get; }

        public MoveInputEvent(double delta, double heldTime, double vX, double vY)
        {
            DeltaTime = delta;
            HeldTime = heldTime;
            VelocityX = vX;
            VelocityY = vY;
            IsStopped = VelocityX == 0 && VelocityY == 0;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"MI[Move:{HeldTime}s|v({VelocityX},{VelocityY})|d{DeltaTime}]";
        }
    }
}
