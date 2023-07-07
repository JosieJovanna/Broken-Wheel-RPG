using BrokenWheel.Control.Models;

namespace BrokenWheel.Control.Interfaces
{
    public interface ITwoHandControl : IOneHandControl
    {
        void TrySecondary(PressData pressData);
    }
}
