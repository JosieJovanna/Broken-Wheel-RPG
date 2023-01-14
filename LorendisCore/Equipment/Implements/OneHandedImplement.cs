using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    public abstract class OneHandedImplement : IImplement
    {
        private readonly ActionBehaviorMap _activeBehaviors;
        private bool _offhanded;
        
        public OneHandedBehaviorMap Behaviors { get; }

        protected OneHandedImplement(OneHandedBehaviorMap behaviors, ActionBehaviorMap activeBehaviorsToModify)
        {
            Behaviors = behaviors;
            _activeBehaviors = activeBehaviorsToModify;
        }
        
        public void Equip(bool offhand = false)
        {
            _offhanded = offhand;
            if (_offhanded)
                SetOffHand();
            else
                SetMainHand();
            SetOthers();
        }

        public void Unequip()
        {
            if (_offhanded)
                ResetMainHand();
            else
                ResetOffHand();
            ResetOthers();
        }

        private void SetMainHand()
        {
            if (Behaviors.Primary != null)
                _activeBehaviors.MainPrimary = Behaviors.Primary;
            if (Behaviors.Secondary != null)
                _activeBehaviors.MainSecondary = Behaviors.Secondary;
        }

        private void ResetMainHand()
        {
            if (Behaviors.Primary != null)
                _activeBehaviors.MainPrimary = null;
            if (Behaviors.Secondary != null)
                _activeBehaviors.MainSecondary = null;
        }

        private void SetOffHand()
        {
            if (Behaviors.Primary != null)
                _activeBehaviors.OffhandPrimary = Behaviors.Primary;
            if (Behaviors.Secondary != null)
                _activeBehaviors.OffhandSecondary = Behaviors.Secondary;
        }

        private void ResetOffHand()
        {
            if (Behaviors.Primary != null)
                _activeBehaviors.OffhandPrimary = null;
            if (Behaviors.Secondary != null)
                _activeBehaviors.OffhandSecondary = null;
        }

        private void SetOthers()
        {
            if (Behaviors.Special != null)
                _activeBehaviors.Special = Behaviors.Special;
            if (Behaviors.Reload != null)
                _activeBehaviors.AddReloadDelegate(Behaviors.Reload);
        }

        private void ResetOthers()
        {
            if (Behaviors.Special != null)
                _activeBehaviors.Special = null;
            if (Behaviors.Reload != null)
                _activeBehaviors.RemoveReloadDelegate(Behaviors.Reload);
        }
    }
}
