using NArchitecture.Core.Application.Responses;

namespace Application.Features.LikedProducts.Commands.Create;

public class CreatedLikedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}