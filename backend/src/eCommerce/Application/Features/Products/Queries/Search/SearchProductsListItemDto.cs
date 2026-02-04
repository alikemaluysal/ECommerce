using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Queries.Search;

public class SearchProductsListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? PrimaryImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
