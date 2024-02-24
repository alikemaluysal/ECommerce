using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Queries.GetById;

public class GetByIdOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}