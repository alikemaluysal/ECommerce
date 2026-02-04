
using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusRequest : IDto
{
    public required OrderStatus Status { get; set; }
}
