using BrokenWheel.Control.Events;

namespace BrokenWheel.Control
{
    public interface IInputProcessor
    {
        void ProcessInput(ButtonInputEvent data);
    }
}
