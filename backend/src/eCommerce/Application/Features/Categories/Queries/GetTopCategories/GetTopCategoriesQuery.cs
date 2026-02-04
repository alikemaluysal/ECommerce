using Application.Features.Categories.Rules;
using AutoMapper;
using NArchitecture.Core.Application.Pipelines.Caching;
using MediatR;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetTopCategories;

public class GetTopCategoriesQuery : IRequest<List<GetTopCategoriesResponse>>, ICachableRequest
{

    public bool BypassCache { get; }
    public string? CacheKey => $"GetTopCategories";
    public string? CacheGroupKey => "GetCategories";
    public TimeSpan? SlidingExpiration { get; }

    private const int TopCategoryCount = 5;

    public class GetTopCategoriesQueryHandler : IRequestHandler<GetTopCategoriesQuery, List<GetTopCategoriesResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetTopCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<GetTopCategoriesResponse>> Handle(GetTopCategoriesQuery request, CancellationToken cancellationToken)
        {
            var topCategories = await _categoryRepository.Query()
                .AsNoTracking()
                .Include(c => c.Products)
                .OrderByDescending(c => c.Products.Count)
                .Take(TopCategoryCount)
                .ToListAsync(cancellationToken);


            List<GetTopCategoriesResponse> response = _mapper.Map<List<GetTopCategoriesResponse>>(topCategories);
            return response;
        }
    }
}
