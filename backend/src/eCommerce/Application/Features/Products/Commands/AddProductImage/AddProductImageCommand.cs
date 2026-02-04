using Application.Features.Products.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Products.Commands.AddProductImage;

public class AddProductImageCommand : IRequest<AddedProductImageResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ProductId { get; set; }
    public AddProductImageRequest Dto { get; set; }

    public string[] Roles => [Admin];

    public AddProductImageCommand(Guid productId, AddProductImageRequest dto)
    {
        ProductId = productId;
        Dto = dto;
    }

    public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, AddedProductImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly ImageServiceBase _imageService;

        public AddProductImageCommandHandler(
            IMapper mapper,
            IProductRepository productRepository,
            IProductImageRepository productImageRepository,
            ProductBusinessRules productBusinessRules,
            ImageServiceBase imageService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _productBusinessRules = productBusinessRules;
            _imageService = imageService;
        }

        public async Task<AddedProductImageResponse> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            string imageUrl = await _imageService.UploadAsync(request.Dto.ImageFile);
            
            ProductImage productImage = new()
            {
                ProductId = request.ProductId,
                ImageUrl = imageUrl,
                IsPrimary = false,
                DisplayOrder = request.Dto.DisplayOrder
            };

            await _productImageRepository.AddAsync(productImage);

            AddedProductImageResponse response = _mapper.Map<AddedProductImageResponse>(productImage);
            return response;
        }
    }
}

