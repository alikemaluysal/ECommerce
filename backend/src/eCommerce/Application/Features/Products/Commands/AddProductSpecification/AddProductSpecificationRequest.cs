using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Commands.AddProductSpecification;

public class AddProductSpecificationRequest : IDto
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}
