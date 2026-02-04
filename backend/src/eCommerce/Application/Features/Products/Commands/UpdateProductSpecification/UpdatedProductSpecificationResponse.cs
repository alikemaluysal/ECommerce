using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.UpdateProductSpecification;

public class UpdatedProductSpecificationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
