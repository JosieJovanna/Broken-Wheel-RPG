using BrokenWheel.Core.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Events
{
    internal class MoveInputEvent : GameEvent
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
