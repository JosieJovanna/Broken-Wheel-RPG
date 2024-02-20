using BrokenWheel.Control.Models.InputData;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Control.Events
{
    internal class MoveInputEvent : UncategorizedGameEvent
    {
        public MoveInputData Data { get; }

        public MoveInputEvent(object sender, MoveInputData data)
            : base(sender)
        {
            Data = data;
        }
    }
}
