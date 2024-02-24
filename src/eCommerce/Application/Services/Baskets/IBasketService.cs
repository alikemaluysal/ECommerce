using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Baskets;

public interface IBasketService
{
    Task<Basket?> GetAsync(
        Expression<Func<Basket, bool>> predicate,
        Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Basket>?> GetListAsync(
        Expression<Func<Basket, bool>>? predicate = null,
        Func<IQueryable<Basket>, IOrderedQueryable<Basket>>? orderBy = null,
        Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Basket> AddAsync(Basket basket);
    Task<Basket> UpdateAsync(Basket basket);
    Task<Basket> DeleteAsync(Basket basket, bool permanent = false);
}
