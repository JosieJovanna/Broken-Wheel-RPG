using LorendisCore.Player.Control.Actions;

namespace LorendisCore.Player.Control
{
    public class OneHandControl
    {
        
        
        public readonly IActionBehavior PrimaryBehavior;
        public readonly IActionBehavior SecondaryBehavior;
        public readonly IActionBehavior SpecialBehavior;

        public void Primary(ButtonData buttonData)
        {
            PrimaryBehavior?.Execute(buttonData);
        }

        public void Secondary(ButtonData buttonData)
        {
            SecondaryBehavior?.Execute(buttonData);
        }

        public void Special(ButtonData buttonData)
        {
            SpecialBehavior?.Execute(buttonData);
        }
    }
}