using BrokenWheel.Control.Events;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class LookCursorInputDataTracker : TimedTracker<LookInputEvent>
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;

        public CursorInputEvent GetCursorEvent(int scale)
        {
            return new CursorInputEvent(PositionX, PositionY, scale);
        }

        protected override LookInputEvent GetEvent(double delta)
        {
            var heldTime = GetHeldTime();
            return new LookInputEvent(delta, heldTime, VelocityX, VelocityY);
        }
    }
}
