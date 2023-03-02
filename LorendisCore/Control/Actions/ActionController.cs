using System;
using System.Linq;
using System.Collections.Generic;
using LorendisCore.Control.Implements;
using LorendisCore.Control.Models;
using LorendisCore.Equipment.Implements;
using LorendisCore.Equipment.Implements.WeaponTypes;
using LorendisCore.Player;

namespace LorendisCore.Control.Actions
{
    /// <summary>
    /// The default implementation of <see cref="IActionController"/>
    /// </summary>
    public class ActionController : IActionController
    {
        private readonly EquipmentControl _equipment;
        private readonly IContextControlChecker _context;

        public ActionController(EquipmentControl equipmentControl, IContextControlChecker contextControlChecker)
        {
            _equipment = equipmentControl ?? throw new ArgumentNullException(nameof(equipmentControl));
            _context = contextControlChecker ?? throw new ArgumentNullException(nameof(contextControlChecker));
        }

        #region Context-Sensitive Controls

        public void Interact(PressData press)
        {
            _context.GetInteractControl()?.TryPrimary(press);
        }

        public void Grab(PressData press)
        {
            _context.GetInteractControl()?.TryPrimary(press);
        }

        public void ReloadOrReady(PressData press)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Equippable Controls

        public void MainHand(PressData press) => GetImplementForPrimary()?.TryPrimary(press);

        private IImplementControl GetImplementForPrimary()
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

        private ITwoHandedImplementControl GetTwoHandedImplement()
        {
            if (_equipment.MainHand.IsTwoHanded())
                return _equipment.MainHand.CastToInterface<ITwoHandedImplementControl>();
            if (_equipment.OffHand.IsTwoHanded())
                return _equipment.OffHand.CastToInterface<ITwoHandedImplementControl>();
            return null;
        }

        public void Special(PressData press) => GetSpecials().FirstOrDefault()?.TrySpecial(press);

        public void AltSpecial(PressData press) => GetSpecials().LastOrDefault()?.TrySpecial(press);

        private IEnumerable<ISpecialControl> GetSpecials()
        {
            var specials = new List<ISpecialControl>();
            AddImplementToListIfSpecial(_equipment.MainHand, specials);
            AddImplementToListIfSpecial(_equipment.OffHand, specials);
            return specials;
        }

        private static void AddImplementToListIfSpecial(IImplementControl implementControl, IList<ISpecialControl> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            
            if (implementControl.ImplementsInterface<ISpecialControl>())
                list.Add((ISpecialControl)implementControl);
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