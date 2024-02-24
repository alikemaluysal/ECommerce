using NArchitecture.Core.Application.Dtos;

namespace Application.Features.LikedProducts.Queries.GetList;

public class GetListLikedProductListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}