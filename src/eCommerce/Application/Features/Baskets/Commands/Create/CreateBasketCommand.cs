using Application.Features.Baskets.Constants;
using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Baskets.Constants.BasketsOperationClaims;

namespace Application.Features.Baskets.Commands.Create;

public class CreateBasketCommand : IRequest<CreatedBasketResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }

    public string[] Roles => [Admin, Write, BasketsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBaskets"];

    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, CreatedBasketResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;

        public CreateBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository,
                                         BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<CreatedBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            Basket basket = _mapper.Map<Basket>(request);

            await _basketRepository.AddAsync(basket);

            CreatedBasketResponse response = _mapper.Map<CreatedBasketResponse>(basket);
            return response;
        }
    }
}