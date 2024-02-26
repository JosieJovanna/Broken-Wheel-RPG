using System;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Control.Puppeteering.Implementation
{
    public class MarionetteControl : IMarionetteControl
    {
        private delegate void MarionetteAction(IMarionette marionette);

        private readonly ILogger _logger;

        private IMarionette _mainMarionette;
        private IList<IMarionette> _sideMarionettes = new List<IMarionette>();

        public MarionetteControl(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void SetMainMarionette(IMarionette marionette)
        {
            if (marionette == null)
                throw new ArgumentNullException(nameof(marionette));
            _logger.LogCategoryGood("Control", $"Setting main marionette to {marionette.Name}...");
            _mainMarionette = marionette;
        }

        public void AddSideMarionette(IMarionette marionette)
        {
            if (marionette == null)
                throw new ArgumentNullException(nameof(marionette));
            _logger.LogCategoryGood("Control", $"Adding side marionette {marionette.Name}...");
            if (!_sideMarionettes.Contains(marionette))
                _sideMarionettes.Add(marionette);
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

        public void Action()
        {
            throw new NotImplementedException();
        }

        public void Jump(float strength)
        {
            ForAll(_ => _.Jump(strength));

        }

        private void ForAll(MarionetteAction func)
        {
            func.Invoke(_mainMarionette);
            foreach (var marionette in _sideMarionettes)
                func.Invoke(marionette);
        }
    }
}
