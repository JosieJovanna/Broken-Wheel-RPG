using System;
using LorendisCore.Player;
using LorendisCore.Control.Models;
using LorendisCore.Control.Implements;
using LorendisCore.Control.Extensions;

namespace LorendisCore.Control
{
    /// <summary>
    /// The default implementation of <see cref="IActionController"/>
    /// </summary>
    public class ActionController : IActionController
    {
        private readonly EquipmentControl _equipment;
        private readonly IContextControlChecker _context;
        private readonly ReloadOrReadyController _reloadOrReadyController;

        public ActionController(EquipmentControl equipmentControl, IContextControlChecker contextControlChecker)
        {
            _equipment = equipmentControl ?? throw new ArgumentNullException(nameof(equipmentControl));
            _context = contextControlChecker ?? throw new ArgumentNullException(nameof(contextControlChecker));
            _reloadOrReadyController = new ReloadOrReadyController(_equipment);
        }

        #region Context-Sensitive Controls

        public void Interact(PressData press) => _context.GetInteractControl()?.TryPrimary(press);

        public void Grab(PressData press) => _context.GetInteractControl()?.TryPrimary(press);

        public void ReloadOrReady(PressData press) => _reloadOrReadyController.ReloadOrReady(press);

        #endregion

        #region Equippable Controls

        public void MainHand(PressData press) => GetImplementForPrimary()?.TryPrimary(press);

        private IOneHandControl GetImplementForPrimary()
        {
            var hasOffhand = _equipment.OffHand != null;
            var isOneHandedMainHand = !_equipment.MainHand.IsTwoHanded();
            var isTwoHandedOffhand = _equipment.OffHand.IsTwoHanded();
            var shouldUseOffHand = hasOffhand && isOneHandedMainHand && isTwoHandedOffhand;
            return shouldUseOffHand ? _equipment.OffHand : _equipment.MainHand;
        }

        public void OffHand(PressData press)
        {
            var twoHandImplement = GetTwoHandedImplement();
            if (twoHandImplement != null)
                twoHandImplement.TrySecondary(press);
            else
                _equipment.OffHand?.TryPrimary(press);
        }

        public void Special(PressData press)
        {
            var twoHanded = GetTwoHandedImplement();
            if (twoHanded != null)
                TrySpecialForImplement(twoHanded, press);
            else
                TryOneHandedSpecial(press);
        }

        private void TryOneHandedSpecial(PressData press)
        {
            var implementOne = press.IsAltPress ? _equipment.OffHand : _equipment.MainHand;
            var implementTwo = !press.IsAltPress ? _equipment.MainHand : _equipment.OffHand;
            var isFirstTrySuccessful = TrySpecialForImplement(implementOne, press);
            if (!isFirstTrySuccessful)
                TrySpecialForImplement(implementTwo, press);
        }

        private static bool TrySpecialForImplement(IOneHandControl control, PressData press)
        {
            if (control.TryCastToInterface<ISpecialControl>(out var asSpecial))
                asSpecial.TrySpecial(press);
            else if (control.TryCastToInterface<IVersatileControl>(out var asVersatile))
                asVersatile.TryToggleGrip(press);
            else
                return false;
            return true;
        }

        private ITwoHandControl GetTwoHandedImplement()
        {
            if (_equipment.MainHand.IsTwoHanded())
                return _equipment.MainHand.CastToInterface<ITwoHandControl>();
            if (_equipment.OffHand.IsTwoHanded())
                return _equipment.OffHand.CastToInterface<ITwoHandControl>();
            return null;
        }

        public void UseAbility(PressData press)
        {
            _equipment.Ability?.TryPrimary(press);
        }

        public void Kick(PressData press)
        {
            _equipment.Kick?.TryPrimary(press);
        }

        #endregion
    }
}