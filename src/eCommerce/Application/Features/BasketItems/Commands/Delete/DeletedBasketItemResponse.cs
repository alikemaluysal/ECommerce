using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Commands.Delete;

public class DeletedBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
}