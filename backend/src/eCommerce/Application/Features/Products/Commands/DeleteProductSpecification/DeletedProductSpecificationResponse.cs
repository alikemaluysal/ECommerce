using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.DeleteProductSpecification;

public class DeletedProductSpecificationResponse : IResponse
{
    public Guid Id { get; set; }
}
