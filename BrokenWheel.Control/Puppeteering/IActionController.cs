using BrokenWheel.Control.Events;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.GameModes;

namespace BrokenWheel.Control.Puppeteering
{
    public interface IActionController :
        IEventHandler<MoveInputEvent>,
        IEventHandler<LookInputEvent>,
        IEventHandler<ButtonInputEvent>,
        IEventHandler<GameModeUpdateEvent>
    {
        /// <summary>
        /// Cancels all charging or pending actions.
        /// </summary>
        void CancelPendingActions();

        /// <summary>
        /// Cancels all actions, including ongoing ones.
        /// </summary>
        void CancelAllActions();
    }
}
