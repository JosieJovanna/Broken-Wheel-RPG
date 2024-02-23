using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class MoveInputDataTracker : TimedTracker
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;

        public MoveInputData GetData(double delta)
        {
            var heldTime = GetHeldTime();
            return new MoveInputData(delta, heldTime, VelocityX, VelocityY);
        }
    }
}
