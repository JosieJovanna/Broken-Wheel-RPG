using BrokenWheel.Core.Control.Models;

namespace BrokenWheel.Core.Control.Implements
{
    public interface ITwoHandControl : IOneHandControl
    {
        void TrySecondary(PressData pressData);
    }
}