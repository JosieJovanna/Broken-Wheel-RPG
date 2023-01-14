using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Models
{
    public class OneHandedBehaviorMap
    {
        public IActionBehavior Primary;
        public IActionBehavior Secondary;
        public IActionBehavior Special;
        public SimpleDelegate Reload;
    }
}