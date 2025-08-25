namespace BuildingBlock.SeedWorks;

public interface IDateAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }
    
    DateTimeOffset? ModifiedAt { get; set; }
}

public abstract class DateAuditableEntity<TId> : Entity<TId>, IDateAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    
    public DateTimeOffset? ModifiedAt { get; set; }
}