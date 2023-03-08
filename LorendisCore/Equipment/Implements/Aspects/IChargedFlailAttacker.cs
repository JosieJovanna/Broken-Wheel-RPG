namespace LorendisCore.Equipment.Implements.Aspects
{
    public interface IChargedFlailAttacker
    {
        bool IsChargingFlail { get; }
        
        void StartSwinging();
        void ReleaseSwing();
    }
}