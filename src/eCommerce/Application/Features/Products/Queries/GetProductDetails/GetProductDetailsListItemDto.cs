using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Queries.GetProductDetails;

public class GetProductDetailsListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}
