using LorendisCore.Common.Stats.Processing;
using LorendisCore.Control;

namespace LorendisCore.Entities
{
    public abstract class AbstractEntity
    {
        public IActionController ActionController;
        public IStatBox Stats;
    }
}
