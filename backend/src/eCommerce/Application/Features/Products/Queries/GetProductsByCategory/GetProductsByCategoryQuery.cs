using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Caching;

namespace Application.Features.Products.Queries.GetProductsByCategory;

public class GetProductsByCategoryQuery : IRequest<List<GetProductsByCategoryResponse>>, ICachableRequest
{
    public Guid CategoryId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetProductsByCategory-{CategoryId}";
    public string? CacheGroupKey => "GetProducts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, List<GetProductsByCategoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<GetProductsByCategoryResponse>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.Query()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => p.CategoryId == request.CategoryId)
                .ToListAsync(cancellationToken);

            List<GetProductsByCategoryResponse> response = _mapper.Map<List<GetProductsByCategoryResponse>>(products);
            return response;
        }
    }
}
