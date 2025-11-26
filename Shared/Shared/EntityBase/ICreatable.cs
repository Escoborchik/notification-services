namespace SharedKernel.EntityBase;

public interface ICreatable
{
    DateTime CreatedOnUtc { get; set; }
    Guid? CreatedBy { get; set; }
}