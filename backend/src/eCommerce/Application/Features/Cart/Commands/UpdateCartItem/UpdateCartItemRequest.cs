
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Cart.Commands.UpdateCartItem;

public class UpdateCartItemRequest : IDto
{
    public int Quantity { get; set; }
}
