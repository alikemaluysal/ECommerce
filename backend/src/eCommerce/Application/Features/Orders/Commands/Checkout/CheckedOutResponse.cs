using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Orders.Commands.Checkout;

public class CheckedOutResponse : IResponse
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
}
