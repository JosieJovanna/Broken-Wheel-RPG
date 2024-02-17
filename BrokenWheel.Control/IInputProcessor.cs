using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control
{
    public interface IInputProcessor
    {
        void ProcessInput(InputData data);
    }
}
