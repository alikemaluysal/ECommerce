using Application.Features.Orders.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Orders.Rules;

public class OrderBusinessRules : BaseBusinessRules
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILocalizationService _localizationService;

    public OrderBusinessRules(IOrderRepository orderRepository, ILocalizationService localizationService)
    {
        _orderRepository = orderRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, OrdersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task OrderShouldExistWhenSelected(Order? order)
    {
        if (order == null)
            await throwBusinessException(OrdersBusinessMessages.OrderNotExists);
    }

    public async Task OrderIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetAsync(
            predicate: o => o.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OrderShouldExistWhenSelected(order);
    }

    public async Task CartShouldNotBeEmpty(List<CartItem> cartItems)
    {
        if (cartItems == null || !cartItems.Any())
            await throwBusinessException(OrdersBusinessMessages.CartIsEmpty);
    }

    public async Task ProductShouldHaveEnoughStock(Product product, int requestedQuantity)
    {
        if (product.Stock < requestedQuantity)
            await throwBusinessException(OrdersBusinessMessages.InsufficientStock);

        await Task.CompletedTask;
    }

    public async Task OrderShouldBelongToUser(Order order, Guid userId)
    {
        if (order.UserId != userId)
            await throwBusinessException(OrdersBusinessMessages.OrderDoesNotBelongToUser);
    }

    public async Task UserCanOnlyCancelOrders(OrderStatus newStatus)
    {
        if (newStatus != OrderStatus.Cancelled)
            await throwBusinessException(OrdersBusinessMessages.UserCanOnlyCancelOrders);

        await Task.CompletedTask;
    }

    public async Task OrderStatusTransitionShouldBeValid(OrderStatus currentStatus, OrderStatus newStatus)
    {
        var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
        {
            { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Cancelled } },
            { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled } },
            { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered } },
            { OrderStatus.Delivered, new List<OrderStatus>() }, // Terminal state
            { OrderStatus.Cancelled, new List<OrderStatus>() }  // Terminal state
        };

        if (!validTransitions.ContainsKey(currentStatus) || !validTransitions[currentStatus].Contains(newStatus))
            await throwBusinessException(OrdersBusinessMessages.InvalidOrderStatusTransition);

        await Task.CompletedTask;
    }
}