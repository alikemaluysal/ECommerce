using Application.Features.OrderItems.Constants;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.OrderItems.Constants.OrderItemsOperationClaims;

namespace Application.Features.OrderItems.Commands.Update;

public class UpdateOrderItemCommand : IRequest<UpdatedOrderItemResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string[] Roles => [Admin, Write, OrderItemsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderItems"];

    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, UpdatedOrderItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly OrderItemBusinessRules _orderItemBusinessRules;

        public UpdateOrderItemCommandHandler(IMapper mapper, IOrderItemRepository orderItemRepository,
                                         OrderItemBusinessRules orderItemBusinessRules)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _orderItemBusinessRules = orderItemBusinessRules;
        }

        public async Task<UpdatedOrderItemResponse> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            OrderItem? orderItem = await _orderItemRepository.GetAsync(predicate: oi => oi.Id == request.Id, cancellationToken: cancellationToken);
            await _orderItemBusinessRules.OrderItemShouldExistWhenSelected(orderItem);
            orderItem = _mapper.Map(request, orderItem);

            await _orderItemRepository.UpdateAsync(orderItem!);

            UpdatedOrderItemResponse response = _mapper.Map<UpdatedOrderItemResponse>(orderItem);
            return response;
        }
    }
}