namespace BrokenWheel.Entity.Meta
{
    public class EntityMeta
    {
        public PhysicsMeta Physics { get; set; } = new PhysicsMeta();

        public MovementMeta Movement { get; set; } = new MovementMeta();
    }
}
