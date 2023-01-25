using LorendisCore.Equipment.Implements;

namespace LorendisCore.Player.Control
{
    public class HandController : IHandController
    {
        public IImplement MainHand { get; set; }
        public IImplement OffHand { get; set; }
        
        public bool IsTwoHanded { get; }
        
        public void MainPrimary()
        {
            throw new System.NotImplementedException();
        }

        public void MainSecondary()
        {
            throw new System.NotImplementedException();
        }

        public void OffPrimary()
        {
            throw new System.NotImplementedException();
        }

        public void OffSecondary()
        {
            throw new System.NotImplementedException();
        }

        public void Special()
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }
    }
}