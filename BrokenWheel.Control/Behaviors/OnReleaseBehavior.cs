using System;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Behaviors
{
    public class OnReleaseBehavior : IActionBehavior
    {
        protected Action OnRelease;

        public OnReleaseBehavior(Action onRelease)
        {
            OnRelease = onRelease;
        }

        public virtual void Execute(ButtonInputData data, bool isModified = false)
        {
            if (data.PressType == PressType.Released)
                OnRelease?.Invoke();
        }

        public virtual void Refresh()
        { }
    }
}
