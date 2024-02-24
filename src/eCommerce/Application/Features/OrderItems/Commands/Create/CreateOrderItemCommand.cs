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

namespace Application.Features.OrderItems.Commands.Create;

public class CreateOrderItemCommand : IRequest<CreatedOrderItemResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string[] Roles => [Admin, Write, OrderItemsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderItems"];

    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreatedOrderItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly OrderItemBusinessRules _orderItemBusinessRules;

        public CreateOrderItemCommandHandler(IMapper mapper, IOrderItemRepository orderItemRepository,
                                         OrderItemBusinessRules orderItemBusinessRules)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _orderItemBusinessRules = orderItemBusinessRules;
        }

        public async Task<CreatedOrderItemResponse> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            OrderItem orderItem = _mapper.Map<OrderItem>(request);

            await _orderItemRepository.AddAsync(orderItem);

            CreatedOrderItemResponse response = _mapper.Map<CreatedOrderItemResponse>(orderItem);
            return response;
        }
    }
}