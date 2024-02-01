namespace BrokenWheel.Core.Equipment.Implements.Aspects
{
    public interface IBackSwingAttacker
    {
        bool IsOnBackSwing { get; }

        void SwingBack();
        void SwingForward();
    }
}
