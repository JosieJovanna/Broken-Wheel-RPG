using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    public abstract class OneHandedImplement : IImplement
    {
        private bool _offhanded;

        protected OneHandedBehaviorMap Behaviors = new OneHandedBehaviorMap();

        protected OneHandedImplement() { }

        protected OneHandedImplement(OneHandedBehaviorMap behaviors)
        {
            Behaviors = behaviors;
        }
        
        public void Equip(ActionBehaviorMap behaviorsToSet, bool offhand = false)
        {
            _offhanded = offhand;
            if (_offhanded)
                SetOffHand(behaviorsToSet);
            else
                SetMainHand(behaviorsToSet);
            SetOthers(behaviorsToSet);
        }

        public void Unequip(ActionBehaviorMap behaviorsToSet)
        {
            if (_offhanded)
                ResetMainHand(behaviorsToSet);
            else
                ResetOffHand(behaviorsToSet);
            ResetOthers(behaviorsToSet);
        }

        private void SetMainHand(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Primary != null)
                behaviorsToSet.MainPrimary = Behaviors.Primary;
            if (Behaviors.Secondary != null)
                behaviorsToSet.MainSecondary = Behaviors.Secondary;
        }

        private void ResetMainHand(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Primary != null)
                behaviorsToSet.MainPrimary = null;
            if (Behaviors.Secondary != null)
                behaviorsToSet.MainSecondary = null;
        }

        private void SetOffHand(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Primary != null)
                behaviorsToSet.OffhandPrimary = Behaviors.Primary;
            if (Behaviors.Secondary != null)
                behaviorsToSet.OffhandSecondary = Behaviors.Secondary;
        }

        private void ResetOffHand(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Primary != null)
                behaviorsToSet.OffhandPrimary = null;
            if (Behaviors.Secondary != null)
                behaviorsToSet.OffhandSecondary = null;
        }

        private void SetOthers(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Special != null)
                behaviorsToSet.Special = Behaviors.Special;
            if (Behaviors.Reload != null)
                behaviorsToSet.AddReloadDelegate(Behaviors.Reload);
        }

        private void ResetOthers(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Special != null)
                behaviorsToSet.Special = null;
            if (Behaviors.Reload != null)
                behaviorsToSet.RemoveReloadDelegate(Behaviors.Reload);
        }
    }
}
