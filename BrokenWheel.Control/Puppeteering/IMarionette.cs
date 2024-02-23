using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenWheel.Control.Puppeteering
{
    /// <summary>
    /// A <see cref="IPuppet"/> which also handles actions.
    /// </summary>
    public interface IMarionette : IPuppet
    {
        void Jump(float strength);
        // TODO: action handling
    }
}
