using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int TotalItems { get; set; }
}
