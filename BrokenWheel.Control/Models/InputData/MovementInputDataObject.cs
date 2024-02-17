namespace BrokenWheel.Control.Models.InputData
{
    internal class MovementInputDataObject
    {
        public double HeldTime { get; set; } = 0.0;
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;

        public MovementInputData GetTick(double delta) => new MovementInputData(delta, HeldTime, VelocityX, VelocityY);
    }
}
