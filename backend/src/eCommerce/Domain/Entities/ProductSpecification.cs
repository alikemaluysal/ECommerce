using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProductSpecification : Entity<Guid>
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = default!;
}
