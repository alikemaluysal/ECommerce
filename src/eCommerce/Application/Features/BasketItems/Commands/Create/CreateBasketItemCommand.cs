using Application.Features.BasketItems.Constants;
using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.BasketItems.Constants.BasketItemsOperationClaims;

namespace Application.Features.BasketItems.Commands.Create;

public class CreateBasketItemCommand : IRequest<CreatedBasketItemResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string[] Roles => [Admin, Write, BasketItemsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBasketItems"];

    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, CreatedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;

        public CreateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                         BasketItemBusinessRules basketItemBusinessRules)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
        }

        public async Task<CreatedBasketItemResponse> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem basketItem = _mapper.Map<BasketItem>(request);

            await _basketItemRepository.AddAsync(basketItem);

            CreatedBasketItemResponse response = _mapper.Map<CreatedBasketItemResponse>(basketItem);
            return response;
        }
    }
}