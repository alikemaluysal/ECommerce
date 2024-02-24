using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Queries.GetList;

public class GetListProductListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public Guid CategoryId { get; set; }
}