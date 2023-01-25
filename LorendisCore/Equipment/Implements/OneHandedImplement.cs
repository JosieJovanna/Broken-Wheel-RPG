using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    public abstract class OneHandedImplement : IImplement
    {
        public readonly OneHandedBehaviorMap Behaviors = new OneHandedBehaviorMap();

        protected OneHandedImplement()
        {
            
        }

        protected OneHandedImplement(OneHandedBehaviorMap behaviors)
        {
            Behaviors = behaviors;
        }
    }
}
