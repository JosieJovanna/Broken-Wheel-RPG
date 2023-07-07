namespace BrokenWheel.Core.Equipment.Implements.Aspects
{
    /// <summary>
    /// An implement that can block. If there are other attacks, those can stop the blocking.
    /// </summary>
    public interface ICanBlock
    {
        bool IsBlocking { get; }
        
        void StartBlocking();
        void StopBlocking();
    }
}