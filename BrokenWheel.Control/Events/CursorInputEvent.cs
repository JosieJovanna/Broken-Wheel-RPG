using BrokenWheel.Core.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Events
{
    public class CursorInputEvent : UncategorizedGameEvent
    {
        public CursorInputData Data { get; }

        public CursorInputEvent(object sender, CursorInputData data)
            : base(sender)
        {
            Data = data;
        }
    }
}
