using Application.Features.Products.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Products.Constants.ProductsOperationClaims;

namespace Application.Features.Products.Queries.Search;

public class SearchProductsQuery : IRequest<GetListResponse<SearchProductsListItemDto>>
{
    public SearchProductsRequest Request { get; set; }

    public SearchProductsQuery(SearchProductsRequest request)
    {
        Request = request;
    }

    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, GetListResponse<SearchProductsListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SearchProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<SearchProductsListItemDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _productRepository.Query()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Images.Where(img => img.IsPrimary));

            if (request.Request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.Request.CategoryId);
            }

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(request.Request.SearchTerm))
            {
                string searchTerm = request.Request.SearchTerm.ToLower();
                query = query.Where(p => 
                    p.Name.ToLower().Contains(searchTerm) || 
                    p.Description.ToLower().Contains(searchTerm));
            }

            // Apply price filters
            if (request.Request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.Request.MinPrice.Value);
            }

            if (request.Request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.Request.MaxPrice.Value);
            }

            // Apply stock filter
            if (request.Request.InStock.HasValue)
            {
                if (request.Request.InStock.Value)
                {
                    query = query.Where(p => p.Stock > 0);
                }
                else
                {
                    query = query.Where(p => p.Stock == 0);
                }
            }

            // Apply sorting
            query = request.Request.SortBy switch
            {
                ProductSortBy.PriceLowToHigh => query.OrderBy(p => p.Price),
                ProductSortBy.PriceHighToLow => query.OrderByDescending(p => p.Price),
                ProductSortBy.Newest => query.OrderByDescending(p => p.CreatedDate),
                _ => query.OrderByDescending(p => p.CreatedDate)
            };

            // Paginate
            IPaginate<Product> products = await query.ToPaginateAsync(
                index: request.Request.PageRequest.PageIndex,
                size: request.Request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<SearchProductsListItemDto> response = _mapper.Map<GetListResponse<SearchProductsListItemDto>>(products);
            return response;
        }
    }
}
