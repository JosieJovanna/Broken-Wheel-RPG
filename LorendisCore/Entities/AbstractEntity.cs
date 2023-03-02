using LorendisCore.Common.Stats.Processing;
using LorendisCore.Control;
using LorendisCore.Control.Actions;

namespace LorendisCore.Entities
{
    public abstract class AbstractEntity
    {
        public IActionController ActionController;
        public IStatBox Stats;
    }
}
