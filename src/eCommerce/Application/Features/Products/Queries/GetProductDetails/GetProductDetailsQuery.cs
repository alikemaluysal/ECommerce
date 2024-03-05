using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Products.Queries.GetProductDetails;

public class GetProductDetailsQuery : IRequest<GetListResponse<GetProductDetailsListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, GetListResponse<GetProductDetailsListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly IProductRepository _productRepository;


        public GetProductDetailsQueryHandler(IMapper mapper, ProductBusinessRules productBusinessRules, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
            _productRepository = productRepository;
        }

        public async Task<GetListResponse<GetProductDetailsListItemDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Product> featuredProducts = await _productRepository.GetListAsync(

                  index: request.PageRequest.PageIndex,
                  size: request.PageRequest.PageSize,
                  include: c => c.Include(p => p.Category).Include(p => p.Images),
                  cancellationToken: cancellationToken
         );

            //TODO: Mapping
            var responseItems = featuredProducts.Items.Select(p => new GetProductDetailsListItemDto
            {
                Id = p.Id,
                CategoryName = p.Category.Name,
                ImageUrl = p.Images.FirstOrDefault()?.ImageUrl ?? "",
                Name = p.Name,
                Price = p.Price
            }).ToList();

            var response = new GetListResponse<GetProductDetailsListItemDto>
            {
                Items = responseItems,
                Index = featuredProducts.Index,
                Size = featuredProducts.Size,
                Count = featuredProducts.Count,
                Pages = featuredProducts.Pages,
                HasNext = featuredProducts.HasNext,
                HasPrevious = featuredProducts.HasPrevious
            };
            return response;
        }
    }
}
