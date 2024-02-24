using Application.Features.LikedProducts.Constants;
using Application.Features.LikedProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LikedProducts.Constants.LikedProductsOperationClaims;

namespace Application.Features.LikedProducts.Commands.Create;

public class CreateLikedProductCommand : IRequest<CreatedLikedProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public string[] Roles => [Admin, Write, LikedProductsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLikedProducts"];

    public class CreateLikedProductCommandHandler : IRequestHandler<CreateLikedProductCommand, CreatedLikedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILikedProductRepository _likedProductRepository;
        private readonly LikedProductBusinessRules _likedProductBusinessRules;

        public CreateLikedProductCommandHandler(IMapper mapper, ILikedProductRepository likedProductRepository,
                                         LikedProductBusinessRules likedProductBusinessRules)
        {
            _mapper = mapper;
            _likedProductRepository = likedProductRepository;
            _likedProductBusinessRules = likedProductBusinessRules;
        }

        public async Task<CreatedLikedProductResponse> Handle(CreateLikedProductCommand request, CancellationToken cancellationToken)
        {
            LikedProduct likedProduct = _mapper.Map<LikedProduct>(request);

            await _likedProductRepository.AddAsync(likedProduct);

            CreatedLikedProductResponse response = _mapper.Map<CreatedLikedProductResponse>(likedProduct);
            return response;
        }
    }
}