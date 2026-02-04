using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommand : IRequest<UpdatedOrderStatusResponse>, ISecuredRequest, ILoggableRequest
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public UpdateOrderStatusRequest Dto { get; set; }
    public bool IsAdminRequest { get; set; } = false;

    public string[] Roles => IsAdminRequest ? [Admin] : [];

    public UpdateOrderStatusCommand(Guid orderId, UpdateOrderStatusRequest dto)
    {
        OrderId = orderId;
        Dto = dto;
    }

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, UpdatedOrderStatusResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public UpdateOrderStatusCommandHandler(
            IMapper mapper,
            IOrderRepository orderRepository,
            OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<UpdatedOrderStatusResponse> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _orderRepository.GetAsync(
                predicate: o => o.Id == request.OrderId,
                cancellationToken: cancellationToken);

            await _orderBusinessRules.OrderShouldExistWhenSelected(order);

            if (!request.IsAdminRequest)
            {
                await _orderBusinessRules.OrderShouldBelongToUser(order!, request.UserId);
                
                await _orderBusinessRules.UserCanOnlyCancelOrders(request.Dto.Status);
            }

            await _orderBusinessRules.OrderStatusTransitionShouldBeValid(order!.Status, request.Dto.Status);

            order.Status = request.Dto.Status;
            await _orderRepository.UpdateAsync(order);

            UpdatedOrderStatusResponse response = _mapper.Map<UpdatedOrderStatusResponse>(order);
            return response;
        }
    }
}
