using System.Diagnostics;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class LookInputDataTracker : TimedTracker
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;

        public LookInputData GetData(double delta)
        {
            var heldTime = GetHeldTime();
            return new LookInputData(delta, heldTime, VelocityX, VelocityY, PositionX, PositionY);
        }
    }
}
