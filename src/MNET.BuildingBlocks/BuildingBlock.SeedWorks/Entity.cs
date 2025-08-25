namespace BuildingBlock.SeedWorks;

public interface IEntity<TId>
{
    TId Id { get; set; }
}

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; } = default!;
}