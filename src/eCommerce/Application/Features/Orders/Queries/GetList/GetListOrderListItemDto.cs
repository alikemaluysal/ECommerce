using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Orders.Queries.GetList;

public class GetListOrderListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}