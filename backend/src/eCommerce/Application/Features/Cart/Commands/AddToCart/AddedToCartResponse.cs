using NArchitecture.Core.Application.Responses;

namespace Application.Features.Cart.Commands.AddToCart;

public class AddedToCartResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
