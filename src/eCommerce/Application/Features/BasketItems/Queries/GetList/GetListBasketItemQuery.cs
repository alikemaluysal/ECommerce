using Application.Features.BasketItems.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.BasketItems.Constants.BasketItemsOperationClaims;

namespace Application.Features.BasketItems.Queries.GetList;

public class GetListBasketItemQuery : IRequest<GetListResponse<GetListBasketItemListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBasketItems({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBasketItems";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBasketItemQueryHandler : IRequestHandler<GetListBasketItemQuery, GetListResponse<GetListBasketItemListItemDto>>
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IMapper _mapper;

        public GetListBasketItemQueryHandler(IBasketItemRepository basketItemRepository, IMapper mapper)
        {
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBasketItemListItemDto>> Handle(GetListBasketItemQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BasketItem> basketItems = await _basketItemRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBasketItemListItemDto> response = _mapper.Map<GetListResponse<GetListBasketItemListItemDto>>(basketItems);
            return response;
        }
    }
}