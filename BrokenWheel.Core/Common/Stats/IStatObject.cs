namespace BrokenWheel.Core.Common.Stats
{
    public interface IStatObject
    {
        string GetStatName();
        
        /// <summary>
        /// Gets the effective value of the stat including any modifiers.
        /// </summary>
        int GetEffectiveValue();
        
        /// <summary>
        /// Sets the effective value of the stat to the given value after modifiers are applied.
        /// </summary>
        void SetEffectiveValue(int val);
        
        int GetValue();
        void SetValue(int val);
        void AddValue(int add);
        
        int GetModifier();
        void SetModifier(int val);
        void AddModifier(int add);
    }
}