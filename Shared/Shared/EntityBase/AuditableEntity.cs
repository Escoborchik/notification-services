using CSharpFunctionalExtensions;

namespace SharedKernel.EntityBase;

public abstract class AuditableEntity<TId>(TId id) : Entity<TId>(id), ICreatable, IUpdatable
where TId : IComparable<TId>
{
    public DateTime CreatedOnUtc { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? LastUpdatedOnUtc { get; set; }
    public Guid? LastUpdatedBy { get; set; }
}
