using BrokenWheel.Control.Models.InputData;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Control.Events
{
    public class MoveInputEvent : GameEvent
    {
        public const string CATEGORY_STOPPED = "Stopped";
        public const string CATEGORY_MOVING = "Moving";

        public MoveInputData Data { get; }

        public MoveInputEvent(object sender, MoveInputData data)
            : base(sender, data.IsStopped ? CATEGORY_STOPPED : CATEGORY_MOVING)
        {
            Data = data;
        }
    }
}
