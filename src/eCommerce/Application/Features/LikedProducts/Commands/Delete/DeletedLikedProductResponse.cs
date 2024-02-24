using NArchitecture.Core.Application.Responses;

namespace Application.Features.LikedProducts.Commands.Delete;

public class DeletedLikedProductResponse : IResponse
{
    public Guid Id { get; set; }
}