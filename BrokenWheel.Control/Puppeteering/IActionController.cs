using BrokenWheel.Control.Events;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Control.Puppeteering
{
    public interface IActionController :
        IEventHandler<MoveInputEvent>,
        IEventHandler<LookInputEvent>,
        IEventHandler<ButtonInputEvent>
    {
    }
}
