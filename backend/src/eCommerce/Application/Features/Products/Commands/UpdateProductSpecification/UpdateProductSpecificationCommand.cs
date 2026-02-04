using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Products.Commands.UpdateProductSpecification;

public class UpdateProductSpecificationCommand : IRequest<UpdatedProductSpecificationResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ProductId { get; set; }
    public Guid SpecId { get; set; }
    public UpdateProductSpecificationRequest Dto { get; set; }

    public string[] Roles => [Admin];

    public UpdateProductSpecificationCommand(Guid productId, Guid specId, UpdateProductSpecificationRequest dto)
    {
        ProductId = productId;
        SpecId = specId;
        Dto = dto;
    }

    public class UpdateProductSpecificationCommandHandler : IRequestHandler<UpdateProductSpecificationCommand, UpdatedProductSpecificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSpecificationRepository _productSpecificationRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public UpdateProductSpecificationCommandHandler(
            IMapper mapper,
            IProductSpecificationRepository productSpecificationRepository,
            ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productSpecificationRepository = productSpecificationRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<UpdatedProductSpecificationResponse> Handle(UpdateProductSpecificationCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            ProductSpecification? productSpecification = await _productSpecificationRepository.GetAsync(
                predicate: ps => ps.Id == request.SpecId && ps.ProductId == request.ProductId,
                cancellationToken: cancellationToken);

            await _productBusinessRules.ProductSpecificationShouldExistWhenSelected(productSpecification);

            productSpecification!.Key = request.Dto.Key;
            productSpecification.Value = request.Dto.Value;

            await _productSpecificationRepository.UpdateAsync(productSpecification);

            UpdatedProductSpecificationResponse response = _mapper.Map<UpdatedProductSpecificationResponse>(productSpecification);
            return response;
        }
    }
}
