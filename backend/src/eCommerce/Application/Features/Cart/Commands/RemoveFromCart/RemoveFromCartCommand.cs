using Application.Features.Cart.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommand : IRequest<RemovedFromCartResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }

    public string[] Roles => [AppUser];
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, RemovedFromCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly CartBusinessRules _cartBusinessRules;

        public RemoveFromCartCommandHandler(
            IMapper mapper,
            ICartItemRepository cartItemRepository,
            CartBusinessRules cartBusinessRules)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _cartBusinessRules = cartBusinessRules;
        }

        public async Task<RemovedFromCartResponse> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            CartItem? cartItem = await _cartItemRepository.GetAsync(
                predicate: ci => ci.Id == request.ItemId && ci.UserId == request.UserId,
                cancellationToken: cancellationToken);

            await _cartBusinessRules.CartItemShouldExistWhenSelected(cartItem);

            await _cartItemRepository.DeleteAsync(cartItem!);

            RemovedFromCartResponse response = _mapper.Map<RemovedFromCartResponse>(cartItem);
            return response;
        }
    }
}
