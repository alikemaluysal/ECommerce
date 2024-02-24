using Application.Features.OrderItems.Constants;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.OrderItems.Constants.OrderItemsOperationClaims;

namespace Application.Features.OrderItems.Queries.GetById;

public class GetByIdOrderItemQuery : IRequest<GetByIdOrderItemResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdOrderItemQueryHandler : IRequestHandler<GetByIdOrderItemQuery, GetByIdOrderItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly OrderItemBusinessRules _orderItemBusinessRules;

        public GetByIdOrderItemQueryHandler(IMapper mapper, IOrderItemRepository orderItemRepository, OrderItemBusinessRules orderItemBusinessRules)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _orderItemBusinessRules = orderItemBusinessRules;
        }

        public async Task<GetByIdOrderItemResponse> Handle(GetByIdOrderItemQuery request, CancellationToken cancellationToken)
        {
            OrderItem? orderItem = await _orderItemRepository.GetAsync(predicate: oi => oi.Id == request.Id, cancellationToken: cancellationToken);
            await _orderItemBusinessRules.OrderItemShouldExistWhenSelected(orderItem);

            GetByIdOrderItemResponse response = _mapper.Map<GetByIdOrderItemResponse>(orderItem);
            return response;
        }
    }
}