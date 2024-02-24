using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Basket: Entity<Guid>
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<BasketItem> BasketItems { get; set; }
    public virtual User User { get; set; }

}

