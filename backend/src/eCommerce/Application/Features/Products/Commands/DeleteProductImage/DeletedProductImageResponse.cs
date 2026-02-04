using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.DeleteProductImage;

public class DeletedProductImageResponse : IResponse
{
    public Guid Id { get; set; }
}
