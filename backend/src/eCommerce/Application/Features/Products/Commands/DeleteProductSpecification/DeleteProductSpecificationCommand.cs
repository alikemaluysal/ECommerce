using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Products.Commands.DeleteProductSpecification;

public class DeleteProductSpecificationCommand : IRequest<DeletedProductSpecificationResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid ProductId { get; set; }
    public Guid SpecId { get; set; }

    public string[] Roles => [Admin];

    public class DeleteProductSpecificationCommandHandler : IRequestHandler<DeleteProductSpecificationCommand, DeletedProductSpecificationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSpecificationRepository _productSpecificationRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public DeleteProductSpecificationCommandHandler(
            IMapper mapper,
            IProductSpecificationRepository productSpecificationRepository,
            ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productSpecificationRepository = productSpecificationRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<DeletedProductSpecificationResponse> Handle(DeleteProductSpecificationCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            ProductSpecification? productSpecification = await _productSpecificationRepository.GetAsync(
                predicate: ps => ps.Id == request.SpecId && ps.ProductId == request.ProductId,
                cancellationToken: cancellationToken);

            await _productBusinessRules.ProductSpecificationShouldExistWhenSelected(productSpecification);

            await _productSpecificationRepository.DeleteAsync(productSpecification!);

            DeletedProductSpecificationResponse response = _mapper.Map<DeletedProductSpecificationResponse>(productSpecification);
            return response;
        }
    }
}
