namespace CleanArchitecture.SharedKernel.Models;

// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
public abstract class BaseEntity<TId>
    where TId : struct
{
    public TId Id { get; set; }

    public List<BaseDomainEvent> Events { get; } = new();
}
