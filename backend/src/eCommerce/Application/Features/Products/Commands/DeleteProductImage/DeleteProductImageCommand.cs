using Application.Features.Products.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Products.Commands.DeleteProductImage;

public class DeleteProductImageCommand : IRequest<DeletedProductImageResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ProductId { get; set; }
    public Guid ImageId { get; set; }

    public string[] Roles => [Admin];

    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, DeletedProductImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly ImageServiceBase _imageService;

        public DeleteProductImageCommandHandler(
            IMapper mapper,
            IProductImageRepository productImageRepository,
            ProductBusinessRules productBusinessRules,
            ImageServiceBase imageService)
        {
            _mapper = mapper;
            _productImageRepository = productImageRepository;
            _productBusinessRules = productBusinessRules;
            _imageService = imageService;
        }

        public async Task<DeletedProductImageResponse> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            ProductImage? productImage = await _productImageRepository.GetAsync(
                predicate: pi => pi.Id == request.ImageId && pi.ProductId == request.ProductId,
                cancellationToken: cancellationToken);

            await _productBusinessRules.ProductImageShouldExistWhenSelected(productImage);

            await _imageService.DeleteAsync(productImage!.ImageUrl);

            await _productImageRepository.DeleteAsync(productImage!);

            DeletedProductImageResponse response = _mapper.Map<DeletedProductImageResponse>(productImage);
            return response;
        }
    }
}

