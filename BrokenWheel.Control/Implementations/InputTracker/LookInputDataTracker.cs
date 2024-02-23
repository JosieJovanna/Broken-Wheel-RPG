﻿using BrokenWheel.Control.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class LookInputDataTracker : TimedTracker<LookInputEvent>
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;

        public LookInputDataTracker(IRPGInputTracker rpgTracker)
            : base(rpgTracker)
        { }

        protected override LookInputEvent GetEvent(object sender, double delta)
        {
            var data = GetData(delta);
            return new LookInputEvent(sender, data);
        }

        private LookInputData GetData(double delta)
        {
            var heldTime = GetHeldTime();
            return new LookInputData(delta, heldTime, VelocityX, VelocityY, PositionX, PositionY);
        }
    }
}