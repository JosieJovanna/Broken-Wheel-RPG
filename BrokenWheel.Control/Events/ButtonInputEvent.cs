using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models.InputData;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Control.Events
{
    public class ButtonInputEvent : StringEnumSwitchGameEvent<RPGInput>
    {
        public InputData Data { get; }

        public ButtonInputEvent(object sender, InputData inputData)
            : base(sender, inputData.Input, inputData.IsCustomInput, inputData.CustomInput)
        {
            Data = inputData;
        }
    }
}
