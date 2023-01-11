using LorendisCore.Common.Stats.Processing;
using LorendisCore.Player.Control.Actions;

namespace LorendisCore.Entities
{
    public abstract class AbstractEntity
    {
        public IActionController ActionController;
        public IStatBox Stats;
    }
}
