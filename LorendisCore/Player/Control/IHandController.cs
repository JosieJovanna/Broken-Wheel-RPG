using LorendisCore.Equipment.Implements;

namespace LorendisCore.Player.Control
{
    public interface IHandController
    {
        IImplement MainHand { get; set; }
        IImplement OffHand { get; set; }
        
        bool IsTwoHanded { get; }
        
        void MainPrimary();
        void MainSecondary();
        void OffPrimary();
        void OffSecondary();
        void Special();
        void Reload();
    }
}