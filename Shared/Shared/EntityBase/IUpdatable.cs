namespace SharedKernel.EntityBase;

public interface IUpdatable
{
    DateTime? LastUpdatedOnUtc { get; set; }
    Guid? LastUpdatedBy { get; set; }
}