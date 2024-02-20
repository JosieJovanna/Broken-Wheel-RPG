using BrokenWheel.Core.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Events
{
    public class ButtonInputEvent : GameEvent
    {
        public InputData Data { get; }

        public ButtonInputEvent(object sender, InputData inputData)
            : base(sender, (inputData.IsCustomInput ? inputData.CustomInput : inputData.Input.ToString()))
        {
            Data = inputData;
        }
    }
}
