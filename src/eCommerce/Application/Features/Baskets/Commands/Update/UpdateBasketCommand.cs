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

namespace Application.Features.Baskets.Commands.Update;

public class UpdateBasketCommand : IRequest<UpdatedBasketResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }

    public string[] Roles => [Admin, Write, BasketsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBaskets"];

    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, UpdatedBasketResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;

        public UpdateBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository,
                                         BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<UpdatedBasketResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);
            basket = _mapper.Map(request, basket);

            await _basketRepository.UpdateAsync(basket!);

            UpdatedBasketResponse response = _mapper.Map<UpdatedBasketResponse>(basket);
            return response;
        }
    }
}