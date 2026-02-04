using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Cart.Queries.GetCart;

public class GetCartQuery : IRequest<GetCartResponse>, ISecuredRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => [AppUser];

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, GetCartResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepository _cartItemRepository;

        public GetCartQueryHandler(
            IMapper mapper,
            ICartItemRepository cartItemRepository)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<GetCartResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.Query()
                .AsNoTracking()
                .Include(ci => ci.Product)
                    .ThenInclude(p => p.Images.Where(i => i.IsPrimary))
                .Where(ci => ci.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var response = new GetCartResponse
            {
                Items = _mapper.Map<List<CartItemDto>>(cartItems),
                TotalAmount = cartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                TotalItems = cartItems.Sum(ci => ci.Quantity)
            };

            return response;
        }
    }
}
