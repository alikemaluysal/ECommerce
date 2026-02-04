using Application.Features.Cart.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommand : IRequest<AddedToCartResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public string[] Roles => [AppUser];

    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, AddedToCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly CartBusinessRules _cartBusinessRules;

        public AddToCartCommandHandler(
            IMapper mapper,
            ICartItemRepository cartItemRepository,
            CartBusinessRules cartBusinessRules)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _cartBusinessRules = cartBusinessRules;
        }

        public async Task<AddedToCartResponse> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            await _cartBusinessRules.QuantityShouldBeValid(request.Quantity);
            await _cartBusinessRules.ProductShouldExistAndHaveStock(request.ProductId, request.Quantity, cancellationToken);
            
            CartItem? existingCartItem = await _cartItemRepository.GetAsync(
                predicate: ci => ci.UserId == request.UserId && ci.ProductId == request.ProductId,
                cancellationToken: cancellationToken);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += request.Quantity;
                await _cartItemRepository.UpdateAsync(existingCartItem);
                
                AddedToCartResponse response = _mapper.Map<AddedToCartResponse>(existingCartItem);
                return response;
            }
            else
            {
                CartItem cartItem = new()
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                await _cartItemRepository.AddAsync(cartItem);

                AddedToCartResponse response = _mapper.Map<AddedToCartResponse>(cartItem);
                return response;
            }
        }
    }
}
