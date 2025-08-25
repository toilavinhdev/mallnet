namespace BuildingBlock.SeedWorks;

public interface IAuditableEntity<TId> : IEntity<TId>, IModifierAuditableEntity, IDateAuditableEntity;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public string? CreatedBy { get; set; }
    
    public string? ModifiedBy { get; set; }
}