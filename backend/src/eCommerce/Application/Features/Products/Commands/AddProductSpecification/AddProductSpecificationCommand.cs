using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Products.Commands.AddProductSpecification;

public class AddProductSpecificationCommand : IRequest<AddedProductSpecificationResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ProductId { get; set; }
    public AddProductSpecificationRequest Dto { get; set; }

    public string[] Roles => [Admin];

    public AddProductSpecificationCommand(Guid productId, AddProductSpecificationRequest dto)
    {
        ProductId = productId;
        Dto = dto;
    }

    public class AddProductSpecificationCommandHandler : IRequestHandler<AddProductSpecificationCommand, AddedProductSpecificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSpecificationRepository _productSpecificationRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public AddProductSpecificationCommandHandler(
            IMapper mapper,
            IProductSpecificationRepository productSpecificationRepository,
            ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productSpecificationRepository = productSpecificationRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<AddedProductSpecificationResponse> Handle(AddProductSpecificationCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            ProductSpecification productSpecification = new()
            {
                ProductId = request.ProductId,
                Key = request.Dto.Key,
                Value = request.Dto.Value
            };

            await _productSpecificationRepository.AddAsync(productSpecification);

            AddedProductSpecificationResponse response = _mapper.Map<AddedProductSpecificationResponse>(productSpecification);
            return response;
        }
    }
}
