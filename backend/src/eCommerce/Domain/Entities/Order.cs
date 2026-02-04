using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Order : Entity<Guid>
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingCountry { get; set; } = string.Empty;
    public string ShippingPostalCode { get; set; } = string.Empty;

    public virtual User User { get; set; } = default!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

}
