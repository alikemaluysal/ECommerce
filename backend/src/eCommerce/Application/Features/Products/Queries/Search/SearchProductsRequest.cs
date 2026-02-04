using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Application.Requests;

namespace Application.Features.Products.Queries.Search;

public class SearchProductsRequest : IDto
{
    public string? SearchTerm { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? InStock { get; set; }
    public Guid? CategoryId { get; set; }
    public ProductSortBy SortBy { get; set; } = ProductSortBy.Newest;
    public PageRequest PageRequest { get; set; } = new() { PageIndex = 0, PageSize = 10 };
}
