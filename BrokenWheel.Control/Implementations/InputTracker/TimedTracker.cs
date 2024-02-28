using System;
using System.Diagnostics;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Events.Observables;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal abstract class TimedTracker<TEvent> where TEvent : GameEvent
    {
        private readonly IRPGInputTracker _tracker;
        private readonly Stopwatch _timer = new Stopwatch();

        private bool _isStopped = true;
        private bool _wasStopped = true;

        public TimedTracker(IRPGInputTracker rpgTracker)
        {
            _tracker = rpgTracker ?? throw new ArgumentNullException(nameof(rpgTracker));
        }

        protected abstract TEvent GetEvent(object sender, double delta);

        public void Pause()
        {
            _timer.Stop();
        }

        public void Resume()
        {
            _timer.Start();
        }

        public void EmitEvent(IEventSubject<TEvent> subject, double delta)
        {
            var @event = GetEvent(_tracker, delta);
            subject.Emit(@event);
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
