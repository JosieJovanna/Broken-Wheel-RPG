using BrokenWheel.Control.Models.InputData;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Control.Events
{
    internal class MoveInputEvent : GameEvent
    {
        public MoveInputData Data { get; }

        public MoveInputEvent(object sender, MoveInputData data)
            : base(sender, MoveInputData.CATEGORY)
        {
            Data = data;
        }
    }
}
