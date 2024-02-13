using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Models
{
    internal class InputDataObject
    {
        public RPGInput Input { get; set; }
        public PressType PressType { get; set; } = PressType.NotHeld;
        public double HeldTime { get; set; } = 0;

        public InputDataObject(RPGInput input)
        {
            Input = input;
        }

        public InputData GetTick(double delta) => new InputData(Input, PressType, delta, HeldTime);
    }
}
