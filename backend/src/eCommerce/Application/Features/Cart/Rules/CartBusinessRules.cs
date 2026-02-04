using Application.Features.Cart.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Cart.Rules;

public class CartBusinessRules : BaseBusinessRules
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILocalizationService _localizationService;

    public CartBusinessRules(
        ICartItemRepository cartItemRepository,
        IProductRepository productRepository,
        ILocalizationService localizationService)
    {
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CartBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CartItemShouldExistWhenSelected(CartItem? cartItem)
    {
        if (cartItem == null)
            await throwBusinessException(CartBusinessMessages.CartItemNotExists);
    }

    public async Task ProductShouldExistAndHaveStock(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(
            predicate: p => p.Id == productId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (product == null)
            await throwBusinessException(CartBusinessMessages.ProductNotExists);

        if (product!.Stock < quantity)
            await throwBusinessException(CartBusinessMessages.InsufficientStock);
    }

    public async Task QuantityShouldBeValid(int quantity)
    {
        if (quantity <= 0)
            await throwBusinessException(CartBusinessMessages.InvalidQuantity);

        await Task.CompletedTask;
    }

    public async Task CartItemShouldNotExistForUser(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        CartItem? existingItem = await _cartItemRepository.GetAsync(
            predicate: ci => ci.UserId == userId && ci.ProductId == productId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (existingItem != null)
            await throwBusinessException(CartBusinessMessages.CartItemAlreadyExists);
    }
}
