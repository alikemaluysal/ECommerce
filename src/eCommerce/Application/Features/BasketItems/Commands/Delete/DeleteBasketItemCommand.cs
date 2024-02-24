using Application.Features.BasketItems.Constants;
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

namespace Application.Features.BasketItems.Commands.Delete;

public class DeleteBasketItemCommand : IRequest<DeletedBasketItemResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, BasketItemsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBasketItems"];

    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, DeletedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;

        public DeleteBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                         BasketItemBusinessRules basketItemBusinessRules)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
        }

        public async Task<DeletedBasketItemResponse> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _basketItemRepository.GetAsync(predicate: bi => bi.Id == request.Id, cancellationToken: cancellationToken);
            await _basketItemBusinessRules.BasketItemShouldExistWhenSelected(basketItem);

            await _basketItemRepository.DeleteAsync(basketItem!);

            DeletedBasketItemResponse response = _mapper.Map<DeletedBasketItemResponse>(basketItem);
            return response;
        }
    }
}