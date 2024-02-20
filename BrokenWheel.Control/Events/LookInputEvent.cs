using BrokenWheel.Core.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Events
{
    public class LookInputEvent : GameEvent
    {
        public const string CATEGORY_STOPPED = "Stopped";
        public const string CATEGORY_MOVING = "Moving";

        public LookInputData Data { get; set; }

        public LookInputEvent(object sender, LookInputData data)
            : base(sender, data.IsStopped ? CATEGORY_STOPPED : CATEGORY_MOVING)
        {
            Data = data;
        }
    }
}
