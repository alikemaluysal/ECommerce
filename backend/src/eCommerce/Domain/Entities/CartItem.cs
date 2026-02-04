using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class CartItem : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public virtual User User { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;
}
