using BrokenWheel.Entity.Meta.Movement;

namespace BrokenWheel.Entity.Meta
{
    public class EntityMeta
    {
        public float Height { get; set; } = 1.75f;

        public PhysicsMeta Physics { get; set; } = new PhysicsMeta();

        public MovementMeta Movement { get; set; } = new MovementMeta();
    }
}
