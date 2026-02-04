using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Commands.UpdateProductSpecification;

public class UpdateProductSpecificationRequest : IDto
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}
