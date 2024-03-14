using System;
using System.Collections.Generic;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Behaviors;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Events;
using BrokenWheel.Control.Settings;

namespace BrokenWheel.Control.Puppeteering.Implementation
{
    public class ActionController : IActionController
    {
        private readonly ControlSettings _controlSettings;
        private readonly IMarionetteControl _marionetteCtrl;
        private readonly IDictionary<RPGInput, IActionBehavior> _behaviors = new Dictionary<RPGInput, IActionBehavior>();

        private bool _isModified = false;

        public ActionController(
            ControlSettings controlSettings,
            IMarionetteControl marionetteControl)
        {
            _controlSettings = controlSettings ?? throw new ArgumentNullException(nameof(controlSettings)); // TODO: settings update refresh
            _marionetteCtrl = marionetteControl ?? throw new ArgumentNullException(nameof(marionetteControl));

            // behaviors
            RegisterPlayerMarionetteControlToggleStanceBehavior();
            RegisterPlayerMarionetteControlToggleSpeedBehavior();
        }

        private void RegisterPlayerMarionetteControlToggleSpeedBehavior()
        {
            _behaviors.Add(RPGInput.ToggleSpeed, new MuxBehavior(() => _controlSettings.ToggleSpeedHoldTime)
            {
                OnClick = () =>
                {
                    if (_marionetteCtrl.Speed == MovementSpeed.Normal)
                        _marionetteCtrl.SetSpeed(MovementSpeed.Slower);
                    else
                        _marionetteCtrl.SetSpeed(MovementSpeed.Normal);
                },
                OnHoldStart = () =>
                {
                    _marionetteCtrl.SetSpeed(MovementSpeed.Faster);
                },
                OnRelease = () =>
                {
                    _marionetteCtrl.SetSpeed(MovementSpeed.Normal);
                }
            });
        }

        private void RegisterPlayerMarionetteControlToggleStanceBehavior()
        {
            _behaviors.Add(RPGInput.ToggleStance, new MuxBehavior(() => _controlSettings.ToggleStanceHoldTime)
            {
                OnClick = () =>
                {
                    if (_marionetteCtrl.Stance == MovementStance.Standing)
                        _marionetteCtrl.SetStance(MovementStance.Crouching);
                    else if (_marionetteCtrl.Stance == MovementStance.Crouching)
                        _marionetteCtrl.SetStance(MovementStance.Standing);
                    else
                        _marionetteCtrl.SetStance(MovementStance.Crouching);
                },
                OnHoldStart = () =>
                {
                    if (_marionetteCtrl.Stance == MovementStance.Standing)
                        _marionetteCtrl.SetStance(MovementStance.Crawling);
                    else if (_marionetteCtrl.Stance == MovementStance.Crouching)
                        _marionetteCtrl.SetStance(MovementStance.Crawling);
                    else
                        _marionetteCtrl.SetStance(MovementStance.Standing);
                }
            });
        }

        public void CancelPendingActions()
        {
            // TODO...
        }

        public void CancelAllActions()
        {
            // TODO...
        }

        public void HandleEvent(MoveInputEvent gameEvent)
        {
            _marionetteCtrl.Move(
                horizontal: (float)gameEvent.VelocityX,
                vertical: (float)gameEvent.VelocityY,
                delta: (float)gameEvent.DeltaTime);
        }

        public void HandleEvent(LookInputEvent gameEvent)
        {
            _marionetteCtrl.Look(
                horizontal: (float)gameEvent.VelocityX,
                vertical: (float)gameEvent.VelocityY,
                delta: (float)gameEvent.DeltaTime);
        }

        public void HandleEvent(ButtonInputEvent data)
        {
            if (data.Input == RPGInput.Modifier)
                _isModified = data.PressType == PressType.Clicked || data.PressType == PressType.Held;
            if (data.Input == RPGInput.Action && data.PressType is PressType.Released)
            {
                // TODO: additional logic for strength
                _marionetteCtrl.Jump((float)data.HeldTime); // TODO: generalize input and use behavior
            }
            else if (_behaviors.TryGetValue(data.Input, out var behavior))
                behavior.Execute(data, _isModified);
        }

        public void HandleEvent(GameModeUpdateEvent gameEvent) => CancelPendingActions();
    }
}
