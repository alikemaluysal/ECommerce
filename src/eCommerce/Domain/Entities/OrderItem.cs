using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
