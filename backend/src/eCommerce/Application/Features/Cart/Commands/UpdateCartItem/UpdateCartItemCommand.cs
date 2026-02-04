using Application.Features.Cart.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Cart.Commands.UpdateCartItem;

public class UpdateCartItemCommand : IRequest<UpdatedCartItemResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public UpdateCartItemRequest Dto { get; set; }

    public string[] Roles => [AppUser];
    public UpdateCartItemCommand(Guid itemId, Guid userId, UpdateCartItemRequest dto)
    {
        ItemId = itemId;
        UserId = userId;
        Dto = dto;
    }

    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, UpdatedCartItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly CartBusinessRules _cartBusinessRules;

        public UpdateCartItemCommandHandler(
            IMapper mapper,
            ICartItemRepository cartItemRepository,
            CartBusinessRules cartBusinessRules)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _cartBusinessRules = cartBusinessRules;
        }

        public async Task<UpdatedCartItemResponse> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            await _cartBusinessRules.QuantityShouldBeValid(request.Dto.Quantity);

            CartItem? cartItem = await _cartItemRepository.GetAsync(
                predicate: ci => ci.Id == request.ItemId && ci.UserId == request.UserId,
                cancellationToken: cancellationToken);

            await _cartBusinessRules.CartItemShouldExistWhenSelected(cartItem);
            await _cartBusinessRules.ProductShouldExistAndHaveStock(cartItem!.ProductId, request.Dto.Quantity, cancellationToken);

            cartItem.Quantity = request.Dto.Quantity;
            await _cartItemRepository.UpdateAsync(cartItem);

            UpdatedCartItemResponse response = _mapper.Map<UpdatedCartItemResponse>(cartItem);
            return response;
        }
    }
}
