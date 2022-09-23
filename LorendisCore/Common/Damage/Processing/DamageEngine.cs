using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace LorendisCore.Common.Damage.Processing
{
    /// <summary>
    /// Handles the pipeline of damage from start to finish.
    /// Processes <see cref="ResistanceApplier"/> to <see cref="DamageCalculator"/> to <see cref="DamageApplier"/>.
    /// </summary>
    public interface DamageEngine
    {
        // takes damage events and queues them
        // continuously calculates and applies damage
        // tracks values on the stat bars
        // calls events when stats cross zero
    }
}
