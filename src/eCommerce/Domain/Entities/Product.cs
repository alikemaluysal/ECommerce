using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }

    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual List<Comment> Comments { get; set; }
    public virtual List<ProductImage> Images { get; set; }

}
