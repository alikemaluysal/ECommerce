using NArchitecture.Core.Application.Responses;

namespace Application.Features.Cart.Commands.RemoveFromCart;

public class RemovedFromCartResponse : IResponse
{
    public Guid Id { get; set; }
}
