﻿using System;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Events;

namespace BrokenWheel.Control.Behaviors
{
    public class OnPressBehavior : IActionBehavior
    {
        protected Action OnInitialPress;

        public OnPressBehavior(Action onInitialPress)
        {
            OnInitialPress = onInitialPress;
        }

        public virtual void Execute(ButtonInputEvent data, bool isModified = false)
        {
            if (data.PressType == PressType.Clicked)
                OnInitialPress?.Invoke(); // TODO: differentiate by modifier?
        }

        public virtual void Refresh()
        { }
    }
}
