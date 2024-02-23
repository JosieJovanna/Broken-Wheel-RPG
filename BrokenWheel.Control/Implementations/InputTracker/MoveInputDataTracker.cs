using BrokenWheel.Control.Events;
using BrokenWheel.Control.Models.InputData;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    internal class MoveInputDataTracker : TimedTracker<MoveInputEvent>
    {
        public double VelocityX { get; set; } = 0.0;
        public double VelocityY { get; set; } = 0.0;

        public MoveInputDataTracker(IRPGInputTracker rpgTracker)
            : base(rpgTracker)
        { }

        protected override MoveInputEvent GetEvent(object sender, double delta)
        {
            var data = GetData(delta);
            return new MoveInputEvent(sender, data);
        }

        private MoveInputData GetData(double delta)
        {
            var heldTime = GetHeldTime();
            return new MoveInputData(delta, heldTime, VelocityX, VelocityY);
        }
    }
}
