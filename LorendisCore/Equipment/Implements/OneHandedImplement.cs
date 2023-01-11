using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    public abstract class OneHandedImplement : IImplement
    {
        private readonly ActionBehaviorMap _activeBehaviors;
        
        public OneHandedBehaviorMap Behaviors { get; set; }

        protected OneHandedImplement(ActionBehaviorMap activeBehaviorsToModify)
        {
            _activeBehaviors = activeBehaviorsToModify;
        }
        
        public void Equip(bool offhand = false)
        {
            if (offhand)
            {
                if (Behaviors.Primary != null)
                    _activeBehaviors.OffhandPrimary = Behaviors.Primary;
                if (Behaviors.Secondary != null)
                    _activeBehaviors.OffhandSecondary = Behaviors.Secondary;
            }
            else
            {
                if (Behaviors.Primary != null)
                    _activeBehaviors.MainPrimary = Behaviors.Primary;
                if (Behaviors.Secondary != null)
                    _activeBehaviors.MainSecondary = Behaviors.Secondary;
            }
            if (Behaviors.Special != null)
                _activeBehaviors.Special = Behaviors.Special;
            if (Behaviors.Reload != null) 
                _activeBehaviors.Reload = Behaviors.Reload;
        }
    }
}
