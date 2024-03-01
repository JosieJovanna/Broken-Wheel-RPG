using System;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Extensions;
using BrokenWheel.Control.Interfaces;
using BrokenWheel.Control.Models;

namespace BrokenWheel.Control.Implementations
{
    /// <summary>
    /// The default implementation of <see cref="IActionController"/>
    /// </summary>
    public class ActionController : IActionController
    {
        private readonly EquipmentControl _equipment;
        private readonly ReloadOrReadyController _reloadOrReadyController;
        private IOneHandControl _interactControl;
        private IOneHandControl _grabControl;

        public ActionController(Core.Events.Observables.IEventAggregator eventAggregator, EquipmentControl equipmentControl)
        {
            _equipment = equipmentControl ?? throw new ArgumentNullException(nameof(equipmentControl));
            _reloadOrReadyController = new ReloadOrReadyController(_equipment);
        }

        public void SetInteractControl(IOneHandControl interactControl) => _interactControl = interactControl;
        public void ResetInteractControl() => _interactControl = null;
        public void SetGrabControl(IOneHandControl kickControl) => _grabControl = kickControl;
        public void ResetGrabControl() => _grabControl = null;

        public void Interact(PressData press) => _interactControl?.TryPrimary(press);
        public void Grab(PressData press) => _grabControl?.TryPrimary(press);
        public void ReloadOrReady(PressData press) => _reloadOrReadyController.ReloadOrReady(press);
        public void UseAbility(PressData press) => _equipment.Ability?.TryPrimary(press);
        public void Kick(PressData press) => _equipment.Kick?.TryPrimary(press);

        /// <summary>
        /// Uses the implement in the main hand. If there is a two-handed implement, will use the primary action.
        /// Will be overridden if the offhand is a versatile weapon and in two-handed mode;
        /// and also if there is no main hand equipped, and the offhand is adaptable -- in which case,
        /// it will use that adaptable weapon's secondary action.
        /// </summary>
        public void MainHand(PressData press)
        {
            var implementToUse = GetImplementForPrimary();
            if (implementToUse != null)
                UsePrimaryOrOverrideIfAdaptable(press, implementToUse);
            else if (_equipment.OffHand.TryCastToInterface<IAdaptableControl>(out var asAdaptableControl))
                asAdaptableControl.TryOverriddenSecondary(press);
        }

        private IOneHandControl GetImplementForPrimary()
        {
            var hasOffhand = _equipment.OffHand != null;
            var isOneHandedMainHand = !_equipment.MainHand.IsTwoHanded();
            var isTwoHandedOffhand = _equipment.OffHand.IsTwoHanded();
            var shouldUseOffHand = hasOffhand && isOneHandedMainHand && isTwoHandedOffhand;
            return shouldUseOffHand ? _equipment.OffHand : _equipment.MainHand;
        }

        private static void UsePrimaryOrOverrideIfAdaptable(PressData press, IOneHandControl implementToUse)
        {
            if (implementToUse.TryCastToInterface<IAdaptableControl>(out var asAdaptableControl2))
                asAdaptableControl2.TryOverriddenPrimary(press);
            else
                implementToUse.TryPrimary(press);
        }

        /// <summary>
        /// Uses the implement in the offhand. If there is a two-handed weapon, will use the secondary action.
        /// Will be overridden if the main hand is a versatile weapon and in two-handed mode;
        /// and also if there is no offhand equipped, and the main hand is adaptable -- in which case,
        /// it will use that adaptable weapon's secondary action.
        /// </summary>
        public void OffHand(PressData press)
        {
            var twoHandImplement = GetTwoHandedImplement();
            if (twoHandImplement != null)
                twoHandImplement.TrySecondary(press);
            else
                _equipment.OffHand?.TryPrimary(press);
        }

        private ITwoHandControl GetTwoHandedImplement()
        {
            if (_equipment.MainHand.IsTwoHanded())
                return _equipment.MainHand.CastToInterface<ITwoHandControl>();
            if (_equipment.OffHand.IsTwoHanded())
                return _equipment.OffHand.CastToInterface<ITwoHandControl>();
            return null;
        }

        /// <summary>
        /// Uses one of the equipped implements' special actions, if applicable.
        /// If there is a two-handed weapon equipped, it will only try to use that one's special.
        /// If there are two one-handed weapons with specials, an alt-press will prioritize the offhand's special,
        /// while the normal press will prioritize the main hand's special.
        /// If only one has a special, that special will always be used.
        /// If there are no weapons with specials, nothing will happen.
        /// </summary>
        public void Special(PressData press)
        {
            var twoHanded = GetTwoHandedImplement();
            if (twoHanded != null)
                TryTwoHandedSpecial(twoHanded, press);
            else
                TryOneHandedSpecial(press);
        }

        private static bool TryTwoHandedSpecial(IOneHandControl control, PressData press)
        {
            if (control.TryCastToInterface<ISpecialControl>(out var asSpecial))
                asSpecial.TrySpecial(press);
            else if (control.TryCastToInterface<IVersatileControl>(out var asVersatile))
                asVersatile.TryToggleGrip(press);
            else
                return false;
            return true;
        }

        private void TryOneHandedSpecial(PressData press)
        {
            var implementOne = press.IsAltPress ? _equipment.OffHand : _equipment.MainHand;
            var implementTwo = !press.IsAltPress ? _equipment.MainHand : _equipment.OffHand;
            var isFirstTrySuccessful = TryTwoHandedSpecial(implementOne, press);
            if (!isFirstTrySuccessful)
                TryTwoHandedSpecial(implementTwo, press);
        }
    }
}
