using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Products.Commands.SetPrimaryImage;

public class SetPrimaryImageCommand : IRequest<SetPrimaryImageResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ProductId { get; set; }
    public Guid ImageId { get; set; }

    public string[] Roles => [Admin];

    public class SetPrimaryImageCommandHandler : IRequestHandler<SetPrimaryImageCommand, SetPrimaryImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public SetPrimaryImageCommandHandler(
            IMapper mapper,
            IProductImageRepository productImageRepository,
            ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productImageRepository = productImageRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<SetPrimaryImageResponse> Handle(SetPrimaryImageCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            ProductImage? newPrimaryImage = await _productImageRepository.GetAsync(
                predicate: pi => pi.Id == request.ImageId && pi.ProductId == request.ProductId,
                cancellationToken: cancellationToken);

            await _productBusinessRules.ProductImageShouldExistWhenSelected(newPrimaryImage);

            var currentPrimaryImages = await _productImageRepository.Query()
                .Where(pi => pi.ProductId == request.ProductId && pi.IsPrimary)
                .ToListAsync(cancellationToken);

            foreach (var image in currentPrimaryImages)
            {
                image.IsPrimary = false;
                await _productImageRepository.UpdateAsync(image);
            }

            newPrimaryImage!.IsPrimary = true;
            await _productImageRepository.UpdateAsync(newPrimaryImage);

            SetPrimaryImageResponse response = _mapper.Map<SetPrimaryImageResponse>(newPrimaryImage);
            return response;
        }
    }
}
