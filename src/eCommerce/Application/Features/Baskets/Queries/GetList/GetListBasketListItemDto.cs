using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Baskets.Queries.GetList;

public class GetListBasketListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
}