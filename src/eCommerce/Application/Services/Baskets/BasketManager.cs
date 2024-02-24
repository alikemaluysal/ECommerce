using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Baskets;

public class BasketManager : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly BasketBusinessRules _basketBusinessRules;

    public BasketManager(IBasketRepository basketRepository, BasketBusinessRules basketBusinessRules)
    {
        _basketRepository = basketRepository;
        _basketBusinessRules = basketBusinessRules;
    }

    public async Task<Basket?> GetAsync(
        Expression<Func<Basket, bool>> predicate,
        Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Basket? basket = await _basketRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return basket;
    }

    public async Task<IPaginate<Basket>?> GetListAsync(
        Expression<Func<Basket, bool>>? predicate = null,
        Func<IQueryable<Basket>, IOrderedQueryable<Basket>>? orderBy = null,
        Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Basket> basketList = await _basketRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return basketList;
    }

    public async Task<Basket> AddAsync(Basket basket)
    {
        Basket addedBasket = await _basketRepository.AddAsync(basket);

        return addedBasket;
    }

    public async Task<Basket> UpdateAsync(Basket basket)
    {
        Basket updatedBasket = await _basketRepository.UpdateAsync(basket);

        return updatedBasket;
    }

    public async Task<Basket> DeleteAsync(Basket basket, bool permanent = false)
    {
        Basket deletedBasket = await _basketRepository.DeleteAsync(basket);

        return deletedBasket;
    }
}
