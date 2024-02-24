using Application.Features.LikedProducts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.LikedProducts.Rules;

public class LikedProductBusinessRules : BaseBusinessRules
{
    private readonly ILikedProductRepository _likedProductRepository;
    private readonly ILocalizationService _localizationService;

    public LikedProductBusinessRules(ILikedProductRepository likedProductRepository, ILocalizationService localizationService)
    {
        _likedProductRepository = likedProductRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, LikedProductsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task LikedProductShouldExistWhenSelected(LikedProduct? likedProduct)
    {
        if (likedProduct == null)
            await throwBusinessException(LikedProductsBusinessMessages.LikedProductNotExists);
    }

    public async Task LikedProductIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LikedProduct? likedProduct = await _likedProductRepository.GetAsync(
            predicate: lp => lp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LikedProductShouldExistWhenSelected(likedProduct);
    }
}