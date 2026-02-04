using NArchitecture.Core.Application.Responses;

namespace Application.Features.Cart.Commands.ClearCart;

public class ClearedCartResponse : IResponse
{
    public bool Success { get; set; }
    public int DeletedItemsCount { get; set; }
}
