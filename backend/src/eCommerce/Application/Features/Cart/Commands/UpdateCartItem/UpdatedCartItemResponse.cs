using NArchitecture.Core.Application.Responses;

namespace Application.Features.Cart.Commands.UpdateCartItem;

public class UpdatedCartItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
