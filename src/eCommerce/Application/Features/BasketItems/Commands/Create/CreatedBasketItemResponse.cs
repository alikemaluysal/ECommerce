using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Commands.Create;

public class CreatedBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}