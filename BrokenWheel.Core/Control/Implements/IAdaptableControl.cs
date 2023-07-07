using BrokenWheel.Core.Control.Models;

namespace BrokenWheel.Core.Control.Implements
{
    /// <summary>
    /// An implement that has extra actions when it is the only weapon equipped.
    /// An example would be a pistol; usually click fires, and aims when held, but when it is alone,
    /// the other action becomes aiming, and the regular click is solely for firing.
    /// </summary>
    public interface IAdaptableControl : IOneHandControl
    {
        void TryOverriddenPrimary(PressData press);
        void TryOverriddenSecondary(PressData press);
    }
}