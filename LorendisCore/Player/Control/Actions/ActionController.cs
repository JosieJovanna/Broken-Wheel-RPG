using System;
using System.Linq;
using System.Collections.Generic;
using LorendisCore.Equipment.Implements;
using LorendisCore.Equipment.Implements.WeaponTypes;

namespace LorendisCore.Player.Control.Actions
{
    /// <summary>
    /// The default implementation of <see cref="IActionController"/>
    /// </summary>
    public class ActionController : IActionController
    {
        private readonly EquipmentControl _equipment;
        
        public ActionController(EquipmentControl equipmentControl)
        {
            _equipment = equipmentControl;
        }

        #region Implements
        public void MainHand(PressData press) => GetImplementForPrimary()?.TryPrimary(press);

        public void AltMainHand(PressData press) => GetImplementForPrimary()?.TryAltPrimary(press);

        private IImplement GetImplementForPrimary()
        {
            var shouldUseOffHand = _equipment.OffHand != null 
                                   && !_equipment.MainHand.IsTwoHanded() 
                                   && _equipment.OffHand.IsTwoHanded();
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

        public void AltOffHand(PressData press)
        {
            var twoHandImplement = GetTwoHandedImplement();
            if (twoHandImplement != null)
                twoHandImplement.TryAltSecondary(press);
            else
                _equipment.OffHand?.TryAltPrimary(press);
        }

        private ITwoHandedImplement GetTwoHandedImplement()
        {
            if (_equipment.MainHand.IsTwoHanded())
                return _equipment.MainHand.CastToInterface<ITwoHandedImplement>();
            if (_equipment.OffHand.IsTwoHanded())
                return _equipment.OffHand.CastToInterface<ITwoHandedImplement>();
            return null;
        }

        public void Special(PressData press) => GetSpecials().FirstOrDefault()?.TrySpecial(press);

        public void AltSpecial(PressData press) => GetSpecials().LastOrDefault()?.TrySpecial(press);

        private IEnumerable<ISpecial> GetSpecials()
        {
            var specials = new List<ISpecial>();
            AddImplementToListIfSpecial(_equipment.MainHand, specials);
            AddImplementToListIfSpecial(_equipment.OffHand, specials);
            return specials;
        }

        private static void AddImplementToListIfSpecial(IImplement implement, IList<ISpecial> list)
        {
            if (list == null) 
                throw new ArgumentNullException(nameof(list));
            if (implement.ImplementsInterface<ISpecial>())
                list.Add((ISpecial)implement);
        }
        
        public void ReloadOrReady(PressData press)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        public void Interact(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public void UseAbility(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public void Kick(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public void Grab(PressData press)
        {
            throw new System.NotImplementedException();
        }
    }
}
