using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Queries.GetById;

public class GetByIdBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}