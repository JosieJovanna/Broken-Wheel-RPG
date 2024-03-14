using BrokenWheel.Control.Events;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class MoveInputDataTracker : TimedTracker<MoveInputEvent>
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;

        protected override MoveInputEvent GetEvent(double delta)
        {
            var heldTime = GetHeldTime();
            return new MoveInputEvent(delta, heldTime, VelocityX, VelocityY);
        }
    }
}
