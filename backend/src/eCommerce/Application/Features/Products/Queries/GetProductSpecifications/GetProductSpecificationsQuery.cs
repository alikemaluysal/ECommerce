using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetProductSpecifications;

public class GetProductSpecificationsQuery : IRequest<List<GetProductSpecificationsResponse>>
{
    public Guid ProductId { get; set; }

    public class GetProductSpecificationsQueryHandler : IRequestHandler<GetProductSpecificationsQuery, List<GetProductSpecificationsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductSpecificationRepository _productSpecificationRepository;

        public GetProductSpecificationsQueryHandler(IMapper mapper, IProductSpecificationRepository productSpecificationRepository)
        {
            _mapper = mapper;
            _productSpecificationRepository = productSpecificationRepository;
        }

        public async Task<List<GetProductSpecificationsResponse>> Handle(GetProductSpecificationsQuery request, CancellationToken cancellationToken)
        {
            var specifications = await _productSpecificationRepository.Query()
                .AsNoTracking()
                .Where(ps => ps.ProductId == request.ProductId)
                .ToListAsync(cancellationToken);

            List<GetProductSpecificationsResponse> response = _mapper.Map<List<GetProductSpecificationsResponse>>(specifications);
            return response;
        }
    }
}
