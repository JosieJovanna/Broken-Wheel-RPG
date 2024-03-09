using System;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;
using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Behaviors;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Settings;

namespace BrokenWheel.Control.Puppeteering.Implementation
{
    public class MarionetteControl : IMarionetteControl
    {
        private readonly ILogger _logger;
        private readonly ControlSettings _controlSettings;

        private IMarionette _mainMarionette;
        private IList<IMarionette> _sideMarionettes = new List<IMarionette>();

        public IActionBehavior DefaultBehaviorToggleStance { get; }
        public IActionBehavior DefaultBehaviorToggleSpeed { get; }

        public MovementStance Stance { get; protected set; }

        public MovementSpeed Speed { get; protected set; }

        public MarionetteControl(ILogger logger, ControlSettings controlSettings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _controlSettings = controlSettings ?? throw new ArgumentNullException(nameof(controlSettings));
        }

        public void SetMainMarionette(IMarionette marionette)
        {
            if (marionette == null)
                throw new ArgumentNullException(nameof(marionette));
            _logger.LogCategoryGood("Control", $"Setting main marionette to {marionette.Name}...");
            _mainMarionette = marionette;
            marionette.SetMovementState(Stance, Speed);
        }

        public void AddSideMarionette(IMarionette marionette)
        {
            if (marionette == null)
                throw new ArgumentNullException(nameof(marionette));
            _logger.LogCategoryGood("Control", $"Adding side marionette {marionette.Name}...");
            if (!_sideMarionettes.Contains(marionette))
                _sideMarionettes.Add(marionette);
            marionette.SetMovementState(Stance, Speed);
        }

        public void CutSideMarionette(IMarionette marionette)
        {
            if (marionette == null)
                throw new ArgumentNullException(nameof(marionette));
            _logger.LogCategoryGood("Control", $"Removing side marionette {marionette.Name}...");
            if (_sideMarionettes.Contains(marionette))
                _sideMarionettes.Remove(marionette);
        }

        public void CutAllSideMarionettes()
        {
            _logger.LogCategoryGood("Control", $"Removing all side marionettes...");
            _sideMarionettes = new List<IMarionette>();
        }

        public void CutAllStrings()
        {
            _logger.LogCategoryGood("Control", $"Removing all marionettes...");
            _sideMarionettes = new List<IMarionette>();
            _mainMarionette = null;
        }

        public void Look(float horizontal, float vertical, float delta)
        {
            ForAll(_ => _.Look(tilt: vertical, pan: horizontal, delta: delta));
        }

        public void Move(float horizontal, float vertical, float delta)
        {
            ForAll(_ => _.Move(vDolly: vertical, vTruck: horizontal, delta: delta));
        }

        public void Jump(float strength)
        {
            ForAll(_ => _.Jump(strength));
        }

        public void SetStance(MovementStance stance)
        {
            Stance = stance;
            ForAll(_ => _.SetMovementState(Stance, Speed));
        }

        public void SetSpeed(MovementSpeed speed)
        {
            Speed = speed;
            ForAll(_ => _.SetMovementState(Stance, Speed));
        }

        private delegate void MarionetteAction(IMarionette marionette);

        private void ForAll(MarionetteAction func)
        {
            func.Invoke(_mainMarionette);
            foreach (var marionette in _sideMarionettes)
                func.Invoke(marionette);
        }
    }
}
