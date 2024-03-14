using System.Diagnostics;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal abstract class TimedTracker<TEvent>
    {
        private readonly Stopwatch _timer = new Stopwatch();

        private bool _isStopped = true;
        private bool _wasStopped = true;

        public abstract TEvent GetEvent(double delta);

        public void Pause()
        {
            _timer.Stop();
        }

        public void Resume()
        {
            _timer.Start();
        }

        public void SetIsStopped(bool isStopped)
        {
            _isStopped = isStopped;
            if (IsDeadInput())
                _timer.Reset();
            else if (IsFreshInput())
                _timer.Restart();
        }

        public void Tick()
        {
            _wasStopped = _isStopped;
        }

        public bool IsDeadInput()
        {
            return _isStopped && _wasStopped;
        }

        protected bool IsFreshInput()
        {
            return !_isStopped && _wasStopped;
        }

        protected double GetHeldTime()
        {
            if (!_wasStopped)
                return _timer.Elapsed.TotalSeconds;
            return 0;
        }
    }
}
