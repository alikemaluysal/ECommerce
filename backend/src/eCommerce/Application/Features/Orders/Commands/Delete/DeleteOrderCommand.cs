using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Orders.Commands.Delete;

public class DeleteOrderCommand : IRequest<DeletedOrderResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin];

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeletedOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public DeleteOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,
                                         OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<DeletedOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _orderBusinessRules.OrderShouldExistWhenSelected(order);

            await _orderRepository.DeleteAsync(order!);

            DeletedOrderResponse response = _mapper.Map<DeletedOrderResponse>(order);
            return response;
        }
    }
}