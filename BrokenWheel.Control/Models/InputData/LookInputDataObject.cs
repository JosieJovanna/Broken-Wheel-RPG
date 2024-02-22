namespace BrokenWheel.Control.Models.InputData
{
    internal class LookInputDataObject
    {
        public double HeldTime { get; set; } = 0.0;
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;
        public bool WasStoppedLastTick { get; set; } = true;

        public bool IsStopped() => VelocityX == 0 && VelocityY == 0;

        public LookInputData GetTick(double delta) => new LookInputData(delta, HeldTime, VelocityX, VelocityY, PositionX, PositionY);
    }
}
