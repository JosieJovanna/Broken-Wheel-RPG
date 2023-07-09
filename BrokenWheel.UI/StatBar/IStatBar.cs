using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar
{
    public interface IStatBar
    {
        StatType Type { get; }
        
        void SetPosition(int xPosition, int yPosition);
        
        void UpdateDisplay();
    }
}
