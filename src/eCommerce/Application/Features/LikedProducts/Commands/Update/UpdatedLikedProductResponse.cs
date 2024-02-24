using NArchitecture.Core.Application.Responses;

namespace Application.Features.LikedProducts.Commands.Update;

public class UpdatedLikedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}