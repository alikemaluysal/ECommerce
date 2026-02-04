using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Orders.Commands.Checkout;

public class CheckoutCommand : IRequest<CheckedOutResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public required string ShippingAddress { get; set; }
    public required string ShippingCity { get; set; }
    public required string ShippingCountry { get; set; }
    public required string ShippingPostalCode { get; set; }

    public string[] Roles => [AppUser];

    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, CheckedOutResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public CheckoutCommandHandler(
            IMapper mapper,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository,
            OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<CheckedOutResponse> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.Query()
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            await _orderBusinessRules.CartShouldNotBeEmpty(cartItems);

            foreach (var cartItem in cartItems)
            {
                await _orderBusinessRules.ProductShouldHaveEnoughStock(cartItem.Product, cartItem.Quantity);
            }

            decimal totalAmount = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);

            Order order = new()
            {
                UserId = request.UserId,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending,
                ShippingAddress = request.ShippingAddress,
                ShippingCity = request.ShippingCity,
                ShippingCountry = request.ShippingCountry,
                ShippingPostalCode = request.ShippingPostalCode
            };

            await _orderRepository.AddAsync(order);

            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new()
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                };

                await _orderItemRepository.AddAsync(orderItem);

                cartItem.Product.Stock -= cartItem.Quantity;
                await _productRepository.UpdateAsync(cartItem.Product);

                await _cartItemRepository.DeleteAsync(cartItem);
            }

            CheckedOutResponse response = _mapper.Map<CheckedOutResponse>(order);
            return response;
        }
    }
}
