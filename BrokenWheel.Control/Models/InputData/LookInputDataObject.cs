namespace BrokenWheel.Control.Models.InputData
{
    public class LookInputDataObject
    {
        public double HeldTime { get; set; } = 0.0;
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;
        public int CentreX { get; set; } = 0;
        public int CentreY { get; set; } = 0;
        public int CornerX { get; set; } = -100;
        public int CornerY { get; set; } = -100;

        public LookInputData GetTick(double delta) => new LookInputData(delta, HeldTime, VelocityX, VelocityY, CentreX, CentreY, CornerX, CornerY);
    }
}
