using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Products.Queries.GetListByDynamic;

public class GetListByDynamicProductQuery : IRequest<GetListResponse<GetListByDynamicProductListItemDto>>, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
        
    

    public class GetListByDynamicProductQueryHandler : IRequestHandler<GetListByDynamicProductQuery, GetListResponse<GetListByDynamicProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicProductListItemDto>> Handle(GetListByDynamicProductQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Product> products = await _productRepository.GetListByDynamicAsync(
                dynamic: request.Dynamic,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicProductListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicProductListItemDto>>(products);
            return response;
        }
    }
}
