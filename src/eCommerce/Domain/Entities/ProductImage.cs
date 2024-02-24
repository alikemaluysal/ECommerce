using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProductImage : Entity<Guid>
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; }
    public virtual Product Product { get; set; }
}
