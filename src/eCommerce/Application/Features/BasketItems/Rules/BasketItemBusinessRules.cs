using Application.Features.BasketItems.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.BasketItems.Rules;

public class BasketItemBusinessRules : BaseBusinessRules
{
    private readonly IBasketItemRepository _basketItemRepository;
    private readonly ILocalizationService _localizationService;

    public BasketItemBusinessRules(IBasketItemRepository basketItemRepository, ILocalizationService localizationService)
    {
        _basketItemRepository = basketItemRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BasketItemsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BasketItemShouldExistWhenSelected(BasketItem? basketItem)
    {
        if (basketItem == null)
            await throwBusinessException(BasketItemsBusinessMessages.BasketItemNotExists);
    }

    public async Task BasketItemIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BasketItem? basketItem = await _basketItemRepository.GetAsync(
            predicate: bi => bi.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BasketItemShouldExistWhenSelected(basketItem);
    }
}