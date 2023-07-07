using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokenWheel.Core.Common.Damage.Processing
{
    /// <summary>
    /// Takes in damage events and applies a player's weaknesses and strengths, before calculating damage with <see cref="IDamageCalculator"/>.
    /// </summary>
    public interface IResistanceApplier
    {
    }
}
