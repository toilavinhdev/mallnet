namespace BuildingBlock.SeedWorks;

public interface IModifierAuditableEntity
{
    string? CreatedBy { get; set; }
    
    string? ModifiedBy { get; set; }
}

public abstract class ModifierAuditableEntity<TId> : Entity<TId>, IModifierAuditableEntity
{
    public string? CreatedBy { get; set; }
    
    public string? ModifiedBy { get; set; }
}