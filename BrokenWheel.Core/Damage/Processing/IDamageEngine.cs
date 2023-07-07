using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace BrokenWheel.Core.Damage.Processing
{
    /// <summary>
    /// Handles the pipeline of damage from start to finish.
    /// Processes <see cref="IResistanceApplier"/> to <see cref="IDamageCalculator"/> to <see cref="IDamageApplier"/>.
    /// </summary>
    public interface IDamageEngine
    {
        // takes damage events and queues them
        // continuously calculates and applies damage
        // tracks values on the stat bars
        // calls events when stats cross zero
    }
}
