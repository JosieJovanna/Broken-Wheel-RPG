using BrokenWheel.Control;
using BrokenWheel.Core.Common.Stats.Processing;

namespace BrokenWheel.Entity
{
    public abstract class AbstractEntity
    {
        public IActionController ActionController;
        public IStatBox Stats;
    }
}
