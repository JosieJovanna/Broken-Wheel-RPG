using BrokenWheel.Core.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Events
{
    public class LookInputEvent : GameEvent
    {
        public LookInputData Data { get; set; }

        public LookInputEvent(object sender, LookInputData data)
            : base(sender, data.IsStopped ? LookInputData.CATEGORY_STOPPED : LookInputData.CATEGORY_MOVING)
        {
            Data = data;
        }
    }
}
