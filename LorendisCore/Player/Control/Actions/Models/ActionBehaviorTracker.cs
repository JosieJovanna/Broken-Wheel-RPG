using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LorendisCore.Player.Control.Actions.Models
{
    public class ActionBehaviorTracker
    {
        public IActionBehavior MainPrimary => _mainPrimaries.LastOrDefault();
        public IActionBehavior MainSecondary => _mainSecondaries.LastOrDefault();
        public IActionBehavior OffhandPrimary => _offhandPrimaries.LastOrDefault();
        public IActionBehavior OffhandSecondary => _offhandSecondaries.LastOrDefault();
        public IActionBehavior Special => _specials.LastOrDefault();

        public IActionBehavior Reload; // TODO: make reload capable of reloading both sides. Or no dual wield guns?
        public IActionBehavior Interact => _interacts.LastOrDefault();
        public IActionBehavior Ability => _abilities.LastOrDefault();
        public IActionBehavior Kick => _kicks.LastOrDefault();
        public IActionBehavior Grab => _grabs.LastOrDefault();
        
        private List<IActionBehavior> _mainPrimaries = new List<IActionBehavior>();
        private List<IActionBehavior> _mainSecondaries = new List<IActionBehavior>();
        private List<IActionBehavior> _offhandPrimaries = new List<IActionBehavior>();
        private List<IActionBehavior> _offhandSecondaries = new List<IActionBehavior>();
        private List<IActionBehavior> _specials = new List<IActionBehavior>();

        private List<IActionBehavior> _reloads = new List<IActionBehavior>(); // TODO: make reload capable of reloading both sides. Or no dual wield guns?
        private List<IActionBehavior> _interacts = new List<IActionBehavior>();
        private List<IActionBehavior> _abilities = new List<IActionBehavior>();
        private List<IActionBehavior> _kicks = new List<IActionBehavior>();
        private List<IActionBehavior> _grabs = new List<IActionBehavior>();

        // TODO: setters and unsetters for each behavior
        
        private void PushBehavior(ICollection<IActionBehavior> behaviorList, IActionBehavior newBehavior)
        {
            if (newBehavior == null)
                return;
            behaviorList.Add(newBehavior);
        }

        private void PopBehavior(IList behaviorList)
        {
            var lastIdx = behaviorList.Count - 1;
            if (lastIdx >= 0)
                behaviorList.RemoveAt(lastIdx);
        }
    }
}