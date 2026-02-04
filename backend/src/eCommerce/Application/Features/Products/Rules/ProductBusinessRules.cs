using Application.Features.Products.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Products.Rules;

public class ProductBusinessRules : BaseBusinessRules
{
    private readonly IProductRepository _productRepository;
    private readonly ILocalizationService _localizationService;

    public ProductBusinessRules(IProductRepository productRepository, ILocalizationService localizationService)
    {
        _productRepository = productRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductShouldExistWhenSelected(Product? product)
    {
        if (product == null)
            await throwBusinessException(ProductsBusinessMessages.ProductNotExists);
    }

    public async Task ProductIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductShouldExistWhenSelected(product);
    }

    public async Task ProductImageShouldExistWhenSelected(ProductImage? productImage)
    {
        if (productImage == null)
            await throwBusinessException(ProductsBusinessMessages.ProductImageNotExists);
    }

    public async Task ProductSpecificationShouldExistWhenSelected(ProductSpecification? productSpecification)
    {
        if (productSpecification == null)
            await throwBusinessException(ProductsBusinessMessages.ProductSpecificationNotExists);
    }

    public async Task ProductShouldHaveStockWhenOrdering(Product product, int quantity)
    {
        if (product.Stock < quantity)
            await throwBusinessException(ProductsBusinessMessages.InsufficientStock);
    }

    public async Task CategoryShouldExistWhenCreatingProduct(Guid categoryId, CancellationToken cancellationToken)
    {
        // This will be implemented when we add CategoryRepository dependency
        // For now, we assume the category exists
        await Task.CompletedTask;
    }
}