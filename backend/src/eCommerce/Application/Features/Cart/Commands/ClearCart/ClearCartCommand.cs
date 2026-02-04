using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Cart.Commands.ClearCart;

public class ClearCartCommand : IRequest<ClearedCartResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => [AppUser];
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, ClearedCartResponse>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public ClearCartCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<ClearedCartResponse> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.Query()
                .Where(ci => ci.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            foreach (var item in cartItems)
            {
                await _cartItemRepository.DeleteAsync(item);
            }

            return new ClearedCartResponse { Success = true, DeletedItemsCount = cartItems.Count };
        }
    }
}
