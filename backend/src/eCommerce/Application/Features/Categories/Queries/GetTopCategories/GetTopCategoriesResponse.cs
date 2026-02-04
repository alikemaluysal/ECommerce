using NArchitecture.Core.Application.Responses;

namespace Application.Features.Categories.Queries.GetTopCategories;

public class GetTopCategoriesResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ProductCount { get; set; }
}
