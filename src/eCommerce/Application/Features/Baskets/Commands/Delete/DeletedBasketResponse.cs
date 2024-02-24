using NArchitecture.Core.Application.Responses;

namespace Application.Features.Baskets.Commands.Delete;

public class DeletedBasketResponse : IResponse
{
    public Guid Id { get; set; }
}