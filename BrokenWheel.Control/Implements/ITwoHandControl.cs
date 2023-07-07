using BrokenWheel.Control.Models;

namespace BrokenWheel.Control.Implements
{
    public interface ITwoHandControl : IOneHandControl
    {
        void TrySecondary(PressData pressData);
    }
}
