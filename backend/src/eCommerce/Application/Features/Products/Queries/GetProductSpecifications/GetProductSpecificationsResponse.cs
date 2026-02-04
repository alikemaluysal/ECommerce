using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Queries.GetProductSpecifications;

public class GetProductSpecificationsResponse : IResponse
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
