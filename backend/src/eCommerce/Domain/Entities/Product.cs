using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Product : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = default!;
    public virtual ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();
    public virtual ICollection<ProductSpecification> Specifications { get; set; } = new HashSet<ProductSpecification>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

}
