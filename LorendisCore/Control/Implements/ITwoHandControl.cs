using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    public interface ITwoHandControl : IOneHandControl
    {
        void TrySecondary(PressData pressData);
    }
}