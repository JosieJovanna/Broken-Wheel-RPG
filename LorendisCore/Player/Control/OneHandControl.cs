using LorendisCore.Player.Control.Actions;

namespace LorendisCore.Player.Control
{
    public class OneHandControl
    {
        
        
        public readonly IActionBehavior PrimaryBehavior;
        public readonly IActionBehavior SecondaryBehavior;
        public readonly IActionBehavior SpecialBehavior;

        public void Primary(PressData pressData)
        {
            PrimaryBehavior?.Execute(pressData);
        }

        public void Secondary(PressData pressData)
        {
            SecondaryBehavior?.Execute(pressData);
        }

        public void Special(PressData pressData)
        {
            SpecialBehavior?.Execute(pressData);
        }
    }
}