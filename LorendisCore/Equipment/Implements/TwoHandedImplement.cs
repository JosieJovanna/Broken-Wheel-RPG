using System.Security.Cryptography;
using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    public abstract class TwoHandedImplement : IImplement
    {
        protected TwoHandedBehaviorMap Behaviors = new TwoHandedBehaviorMap();

        protected TwoHandedImplement() { }

        protected TwoHandedImplement(TwoHandedBehaviorMap behaviors)
        {
            Behaviors = behaviors;
        }

        public void Equip(ActionBehaviorMap behaviorsToSet, bool offhand = false)
        {
            SetMainHand(behaviorsToSet);
            SetOffHand(behaviorsToSet);
            SetOthers(behaviorsToSet);
        }

        public void Unequip(ActionBehaviorMap behaviorsToSet)
        {
            behaviorsToSet.MainPrimary = null;
            behaviorsToSet.MainSecondary = null;
            behaviorsToSet.OffhandPrimary = null;
            behaviorsToSet.OffhandSecondary = null;
            behaviorsToSet.Special = null;
            behaviorsToSet.RemoveReloadDelegate(Behaviors.Reload);
        }

        private void SetMainHand(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.MainPrimary != null)
                behaviorsToSet.MainPrimary = Behaviors.MainPrimary;
            if (Behaviors.MainSecondary != null)
                behaviorsToSet.MainSecondary = Behaviors.MainSecondary;
        }

        private void SetOffHand(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.OffhandPrimary != null)
                behaviorsToSet.OffhandPrimary = Behaviors.OffhandPrimary;
            if (Behaviors.OffhandSecondary != null)
                behaviorsToSet.OffhandSecondary = Behaviors.OffhandSecondary;
        }

        private void SetOthers(ActionBehaviorMap behaviorsToSet)
        {
            if (Behaviors.Special != null)
                behaviorsToSet.Special = Behaviors.Special;
            if (Behaviors.Reload != null)
                behaviorsToSet.AddReloadDelegate(Behaviors.Reload);
        }
    }
}