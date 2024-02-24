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

namespace Application.Features.LikedProducts.Commands.Update;

public class UpdateLikedProductCommand : IRequest<UpdatedLikedProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public string[] Roles => [Admin, Write, LikedProductsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLikedProducts"];

    public class UpdateLikedProductCommandHandler : IRequestHandler<UpdateLikedProductCommand, UpdatedLikedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILikedProductRepository _likedProductRepository;
        private readonly LikedProductBusinessRules _likedProductBusinessRules;

        public UpdateLikedProductCommandHandler(IMapper mapper, ILikedProductRepository likedProductRepository,
                                         LikedProductBusinessRules likedProductBusinessRules)
        {
            _mapper = mapper;
            _likedProductRepository = likedProductRepository;
            _likedProductBusinessRules = likedProductBusinessRules;
        }

        public async Task<UpdatedLikedProductResponse> Handle(UpdateLikedProductCommand request, CancellationToken cancellationToken)
        {
            LikedProduct? likedProduct = await _likedProductRepository.GetAsync(predicate: lp => lp.Id == request.Id, cancellationToken: cancellationToken);
            await _likedProductBusinessRules.LikedProductShouldExistWhenSelected(likedProduct);
            likedProduct = _mapper.Map(request, likedProduct);

            await _likedProductRepository.UpdateAsync(likedProduct!);

            UpdatedLikedProductResponse response = _mapper.Map<UpdatedLikedProductResponse>(likedProduct);
            return response;
        }
    }
}