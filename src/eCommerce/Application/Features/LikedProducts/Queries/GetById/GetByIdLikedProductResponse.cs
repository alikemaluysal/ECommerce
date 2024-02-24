using NArchitecture.Core.Application.Responses;

namespace Application.Features.LikedProducts.Queries.GetById;

public class GetByIdLikedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}