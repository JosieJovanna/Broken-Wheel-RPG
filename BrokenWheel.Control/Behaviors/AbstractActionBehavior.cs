using System;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Behaviors
{
    /// <summary>
    /// The simplest type of <see cref="IActionBehavior"/>, where a different action 
    /// can be performed based on whether the button is initially pressed,
    /// when the button has been held for a certain amount of time,
    /// and when the button has been released.
    /// These actions are abstract methods which must be individually implemented.
    /// </summary>
    public abstract class AbstractActionBehavior : IActionBehavior
    {
        private readonly bool _isPermanentHoldTime;
        private readonly Func<double> _holdTimeGetter;
        private double _holdTime;
        private bool _isModifierLocked = false;
        private bool _isHeld = false;

        protected double HeldTime { get; private set; }
        protected bool IsModified { get; private set; }

        /// <summary>
        /// Called when the button is initially depressed.
        /// </summary>
        protected abstract void Press();

        /// <summary>
        /// Called when the button is released before reaching the hold time.
        /// </summary>
        protected abstract void Click();

        /// <summary>
        /// Called on the first frame where the button has been held at least as long as the hold time.
        /// </summary>
        protected abstract void Held();

        /// <summary>
        /// Called on every frame where the button has been held at least as long as the hold time.
        /// </summary>
        protected abstract void Hold();

        /// <summary>
        /// Called when the button is released after having reached the hold time.
        /// </summary>
        protected abstract void Release();

        /// <summary>
        /// Creates an action behavior which differentiates actions based on the hold time.
        /// This time cannot be changed except by creating a new instance of the behavior.
        /// </summary>
        protected AbstractActionBehavior(double permanentHoldTime)
        {
            _isPermanentHoldTime = true;
            _holdTime = permanentHoldTime;
        }

        /// <summary>
        /// Creates an action behavior which differentiates actions based on the hold time.
        /// This time may be updated with settings changes, and so uses a getter function to get the new value on demand.
        /// </summary>
        /// <param name="holdTimeGetter"> The function used to get the hold time on demand. </param>
        /// <exception cref="ArgumentNullException"> If the function is null. </exception>
        protected AbstractActionBehavior(Func<double> holdTimeGetter)
        {
            _isPermanentHoldTime = false;
            _holdTimeGetter = holdTimeGetter ?? throw new ArgumentNullException(nameof(holdTimeGetter));
            _holdTime = _holdTimeGetter.Invoke();
        }

        public void Refresh()
        {
            if (_isPermanentHoldTime)
                return;
            _holdTime = _holdTimeGetter();
        }

        public void Execute(ButtonInputData data, bool isModified = false)
        {
            HeldTime = data.HeldTime;

            if (!_isModifierLocked)
                IsModified = isModified;
            SwitchPress(data);
        }

        private void SwitchPress(ButtonInputData data)
        {
            switch (data.PressType)
            {
                case PressType.Clicked:
                    Press();
                    break;
                case PressType.Released:
                    ReleaseOrClick();
                    break;
                case PressType.Held:
                    HoldIfLongEnough();
                    break;
                case PressType.NotHeld:
                default:
                    _isModifierLocked = false;
                    break;
            }
        }

        private void ReleaseOrClick()
        {
            if (HeldTime >= _holdTime)
                Release();
            else
                Click();
            HeldTime = 0;
            _isModifierLocked = false;
            _isHeld = false;
        }

        private void HoldIfLongEnough()
        {
            if (HeldTime < _holdTime)
                return;

            if (!_isHeld)
            {
                Held();
                _isHeld = true;
            }
            Hold();
        }
    }
}
