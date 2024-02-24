using Application.Features.LikedProducts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.LikedProducts.Constants.LikedProductsOperationClaims;

namespace Application.Features.LikedProducts.Queries.GetList;

public class GetListLikedProductQuery : IRequest<GetListResponse<GetListLikedProductListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListLikedProducts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetLikedProducts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLikedProductQueryHandler : IRequestHandler<GetListLikedProductQuery, GetListResponse<GetListLikedProductListItemDto>>
    {
        private readonly ILikedProductRepository _likedProductRepository;
        private readonly IMapper _mapper;

        public GetListLikedProductQueryHandler(ILikedProductRepository likedProductRepository, IMapper mapper)
        {
            _likedProductRepository = likedProductRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLikedProductListItemDto>> Handle(GetListLikedProductQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LikedProduct> likedProducts = await _likedProductRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLikedProductListItemDto> response = _mapper.Map<GetListResponse<GetListLikedProductListItemDto>>(likedProducts);
            return response;
        }
    }
}