using System.Diagnostics;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class LookInputDataTracker
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;
        public Stopwatch HeldTimer { get; } = new Stopwatch();
        public bool IsStopped { get; set; } = true;
        public bool WasStoppedLastTick { get; set; } = true;

        public LookInputData GetTick(double delta)
        {
            double heldTime = 0;
            if (!WasStoppedLastTick)
                heldTime = HeldTimer.Elapsed.TotalSeconds;
            return new LookInputData(delta, heldTime, VelocityX, VelocityY, PositionX, PositionY);
        }
    }
}
