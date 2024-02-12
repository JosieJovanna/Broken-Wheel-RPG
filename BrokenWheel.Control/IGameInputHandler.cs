using BrokenWheel.Control.Models;

namespace BrokenWheel.Control
{
    /// <summary>
    /// Takes input events, does some logic, and uses them to trigger appropriate actions.
    /// </summary>
    public interface IGameInputHandler
    {
        void HandleInput(InputData data);
        void Process(double deltaTime);
    }
}
