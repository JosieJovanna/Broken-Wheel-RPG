using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Models.InputData
{
    public class ButtonInputDataObject
    {
        public RPGInput Input { get; set; }
        public PressType PressType { get; set; } = PressType.NotHeld;
        public double HeldTime { get; set; } = 0;

        public ButtonInputDataObject(RPGInput input)
        {
            Input = input;
        }

        public ButtonInputData GetTick(double delta) => new ButtonInputData(Input, PressType, delta, HeldTime);
    }
}
