using Application.Features.LikedProducts.Constants;
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

namespace Application.Features.LikedProducts.Commands.Delete;

public class DeleteLikedProductCommand : IRequest<DeletedLikedProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, LikedProductsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetLikedProducts"];

    public class DeleteLikedProductCommandHandler : IRequestHandler<DeleteLikedProductCommand, DeletedLikedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILikedProductRepository _likedProductRepository;
        private readonly LikedProductBusinessRules _likedProductBusinessRules;

        public DeleteLikedProductCommandHandler(IMapper mapper, ILikedProductRepository likedProductRepository,
                                         LikedProductBusinessRules likedProductBusinessRules)
        {
            _mapper = mapper;
            _likedProductRepository = likedProductRepository;
            _likedProductBusinessRules = likedProductBusinessRules;
        }

        public async Task<DeletedLikedProductResponse> Handle(DeleteLikedProductCommand request, CancellationToken cancellationToken)
        {
            LikedProduct? likedProduct = await _likedProductRepository.GetAsync(predicate: lp => lp.Id == request.Id, cancellationToken: cancellationToken);
            await _likedProductBusinessRules.LikedProductShouldExistWhenSelected(likedProduct);

            await _likedProductRepository.DeleteAsync(likedProduct!);

            DeletedLikedProductResponse response = _mapper.Map<DeletedLikedProductResponse>(likedProduct);
            return response;
        }
    }
}