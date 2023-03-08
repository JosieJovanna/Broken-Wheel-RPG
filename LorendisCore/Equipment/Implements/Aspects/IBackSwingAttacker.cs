namespace LorendisCore.Equipment.Implements.Aspects
{
    public interface IBackSwingAttacker
    {
        bool IsOnBackSwing { get; }
        
        void SwingBack();
        void SwingForward();
    }
}