using System.Diagnostics;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class ButtonInputDataTracker
    {
        public RPGInput Input { get; set; }
        public PressType PressType { get; set; } = PressType.NotHeld;
        public bool IsInputThisTick { get; set; } = false;
        public Stopwatch HeldTimer { get; } = new Stopwatch();

        public ButtonInputDataTracker(RPGInput input)
        {
            Input = input;
        }

        public void Reset()
        {
            PressType = PressType.NotHeld;
            IsInputThisTick = false;
            HeldTimer.Reset();
        }

        public ButtonInputData GetData(double delta)
        {
            double heldTime = 0;
            if (PressType == PressType.Held || PressType == PressType.Released)
                heldTime = HeldTimer.Elapsed.TotalSeconds;
            else if (PressType == PressType.Clicked)
                HeldTimer.Restart();
            return new ButtonInputData(Input, PressType, delta, heldTime);
        }
    }
}
