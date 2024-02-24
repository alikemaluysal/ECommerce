using Application.Features.Baskets.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Baskets.Rules;

public class BasketBusinessRules : BaseBusinessRules
{
    private readonly IBasketRepository _basketRepository;
    private readonly ILocalizationService _localizationService;

    public BasketBusinessRules(IBasketRepository basketRepository, ILocalizationService localizationService)
    {
        _basketRepository = basketRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BasketsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BasketShouldExistWhenSelected(Basket? basket)
    {
        if (basket == null)
            await throwBusinessException(BasketsBusinessMessages.BasketNotExists);
    }

    public async Task BasketIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BasketShouldExistWhenSelected(basket);
    }
}