using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProductImage : Entity<Guid>
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = default!;
}
