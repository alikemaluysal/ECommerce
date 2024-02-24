using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Order : Entity<Guid>
{
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public virtual User User { get; set; }
    public virtual List<OrderItem> OrderDetails { get; set; }
}
