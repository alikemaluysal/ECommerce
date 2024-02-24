using NArchitecture.Core.Application.Responses;

namespace Application.Features.Baskets.Commands.Create;

public class CreatedBasketResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
}