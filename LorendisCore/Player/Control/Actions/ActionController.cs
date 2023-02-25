using LorendisCore.Player.Inventory;

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

        public void MainPrimary(PressData press)
        {
        }

        public void MainSecondary(PressData press)
        {
        }

        public void OffhandPrimary(PressData press)
        {
        }

        public void OffhandSecondary(PressData press)
        {
        }

        public void Special(PressData press)
        {
            throw new System.NotImplementedException();
        }

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

        public void Reload(PressData press)
        {
            throw new System.NotImplementedException();
        }
    }
}
