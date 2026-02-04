using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Rules;

public class CategoryBusinessRules : BaseBusinessRules
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILocalizationService _localizationService;

    public CategoryBusinessRules(ICategoryRepository categoryRepository, IProductRepository productRepository, ILocalizationService localizationService)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CategoriesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CategoryShouldExistWhenSelected(Category? category)
    {
        if (category == null)
            await throwBusinessException(CategoriesBusinessMessages.CategoryNotExists);
    }

    public async Task CategoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CategoryShouldExistWhenSelected(category);
    }

    public async Task CategoryNameShouldBeUniqueWhenCreating(string name, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetAsync(
            predicate: c => c.Name.ToLower() == name.ToLower(),
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        if (category != null)
            await throwBusinessException(CategoriesBusinessMessages.CategoryNameAlreadyExists);
    }

    public async Task CategoryNameShouldBeUniqueWhenUpdating(Guid id, string name, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetAsync(
            predicate: c => c.Id != id && c.Name.ToLower() == name.ToLower(),
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        if (category != null)
            await throwBusinessException(CategoriesBusinessMessages.CategoryNameAlreadyExists);
    }

    public async Task CategoryShouldNotHaveProductsWhenDeleting(Guid categoryId, CancellationToken cancellationToken)
    {
        bool hasProducts = await _productRepository
            .Query()
            .AnyAsync(p => p.CategoryId == categoryId, cancellationToken);

        if (hasProducts)
            await throwBusinessException(CategoriesBusinessMessages.CategoryHasProducts);
    }
}