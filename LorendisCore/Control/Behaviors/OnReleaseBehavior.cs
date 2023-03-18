﻿using LorendisCore.Common.Delegates;
using LorendisCore.Control.Models;

namespace LorendisCore.Control.Behaviors
{
    public class OnReleaseBehavior : IActionBehavior
    {
        protected SimpleDelegate OnRelease;

        public OnReleaseBehavior(SimpleDelegate onRelease) 
        {
            OnRelease = onRelease;
        }

        public void Execute(PressData press)
        {
            if (press.Type == PressType.Released)
                OnRelease?.Invoke();
        }
    }
}