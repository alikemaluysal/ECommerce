using Application.Features.LikedProducts.Constants;
using Application.Features.LikedProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LikedProducts.Constants.LikedProductsOperationClaims;

namespace Application.Features.LikedProducts.Queries.GetById;

public class GetByIdLikedProductQuery : IRequest<GetByIdLikedProductResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdLikedProductQueryHandler : IRequestHandler<GetByIdLikedProductQuery, GetByIdLikedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILikedProductRepository _likedProductRepository;
        private readonly LikedProductBusinessRules _likedProductBusinessRules;

        public GetByIdLikedProductQueryHandler(IMapper mapper, ILikedProductRepository likedProductRepository, LikedProductBusinessRules likedProductBusinessRules)
        {
            _mapper = mapper;
            _likedProductRepository = likedProductRepository;
            _likedProductBusinessRules = likedProductBusinessRules;
        }

        public async Task<GetByIdLikedProductResponse> Handle(GetByIdLikedProductQuery request, CancellationToken cancellationToken)
        {
            LikedProduct? likedProduct = await _likedProductRepository.GetAsync(predicate: lp => lp.Id == request.Id, cancellationToken: cancellationToken);
            await _likedProductBusinessRules.LikedProductShouldExistWhenSelected(likedProduct);

            GetByIdLikedProductResponse response = _mapper.Map<GetByIdLikedProductResponse>(likedProduct);
            return response;
        }
    }
}