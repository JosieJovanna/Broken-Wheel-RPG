using BrokenWheel.Core.Common.Stats.Processing;
using BrokenWheel.Core.Control;

namespace BrokenWheel.Core.Entities
{
    public abstract class AbstractEntity
    {
        public IActionController ActionController;
        public IStatBox Stats;
    }
}
