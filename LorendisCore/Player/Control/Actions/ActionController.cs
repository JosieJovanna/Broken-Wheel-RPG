using LorendisCore.Player.Control.Actions.Models;
using LorendisCore.Player.Inventory;

namespace LorendisCore.Player.Control.Actions
{
    /// <summary>
    /// The default implementation of <see cref="IActionController"/>
    /// </summary>
    public class ActionController : IActionController
    {
        private readonly EquipmentModel _equipment;
        
        public ActionController(EquipmentModel equipmentModel)
        {
            _equipment = equipmentModel;
        }

        public void MainPrimary(PressData press)
        {
            if (_equipment.IsTwoHanded)
            {
                _equipment.TwoHand.TryPrimaryPress(press);
            }
            else
            {
                _equipment.MainHand.TryPrimary(press);
            }
        }

        public void MainSecondary(PressData press)
        {
            (Behaviors.MainSecondaryOverride ?? Behaviors.MainSecondary)?.Execute(press);
        }

        public void OffhandPrimary(PressData press)
        {
            (Behaviors.OffhandPrimaryOverride ?? Behaviors.OffhandPrimary)?.Execute(press);
        }

        public void OffhandSecondary(PressData press)
        {
            (Behaviors.OffhandSecondaryOverride ?? Behaviors.OffhandSecondary)?.Execute(press);
        }

        public void Special(PressData press) => (Behaviors.SpecialOverride ?? Behaviors.Special)?.Execute(press);

        public void Interact(PressData press) => Behaviors.Interact?.Execute(press);

        public void UseAbility(PressData press) => Behaviors.Ability?.Execute(press);

        public void Kick(PressData press) => Behaviors.Kick?.Execute(press);

        public void Grab(PressData press) => Behaviors.Grab?.Execute(press);

        public void Reload(PressData press) => Behaviors.Reload.Execute(press);
    }
}
