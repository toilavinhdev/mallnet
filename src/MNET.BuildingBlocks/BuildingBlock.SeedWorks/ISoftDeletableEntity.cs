namespace BuildingBlock.SeedWorks;

public interface ISoftDeletableEntity
{
    DateTimeOffset? DeletedAt { get; set; }
}