using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Queries.GetListByDynamic;

public class GetListByDynamicProductListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }
    public string? PrimaryImageUrl { get; set; }
}
