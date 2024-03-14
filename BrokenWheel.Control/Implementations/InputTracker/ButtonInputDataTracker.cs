using System.Diagnostics;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Events;

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

        /// <summary>
        /// Updates the current press type and notes that there was input this tick.
        /// </summary>
        public void PressThisTick(PressType pressType)
        {
            PressType = pressType;
            IsInputThisTick = true;
        }

        /// <summary>
        /// Resets held-time timer and press type.
        /// </summary>
        public void Reset()
        {
            PressType = PressType.NotHeld;
            IsInputThisTick = false;
            HeldTimer.Reset();
        }

        public ButtonInputEvent GetEvent(double delta)
        {
            double heldTime = 0;
            if (PressType == PressType.Held || PressType == PressType.Released)
                heldTime = HeldTimer.Elapsed.TotalSeconds;
            else if (PressType == PressType.Clicked)
                HeldTimer.Restart();
            return new ButtonInputEvent(Input, PressType, delta, heldTime);
        }
    }
}
