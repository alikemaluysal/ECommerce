using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Orders.Commands.UpdateOrderStatus;

public class UpdatedOrderStatusResponse : IResponse
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}
