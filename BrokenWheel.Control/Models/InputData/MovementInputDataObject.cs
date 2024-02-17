namespace BrokenWheel.Control.Models.InputData
{
    public class MovementInputDataObject
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;

        public MovementInputData GetTick(double delta) => new MovementInputData(delta, VelocityX, VelocityY);
    }
}
