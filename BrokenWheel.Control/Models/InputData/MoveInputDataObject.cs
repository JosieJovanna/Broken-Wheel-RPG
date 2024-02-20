namespace BrokenWheel.Control.Models.InputData
{
    internal class MoveInputDataObject
    {
        public double HeldTime { get; set; } = 0.0;
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;

        public bool IsStopped() => VelocityX == 0 && VelocityY == 0;

        public MoveInputData GetTick(double delta) => new MoveInputData(delta, HeldTime, VelocityX, VelocityY);
    }
}
