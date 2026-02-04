using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CartItems;

public interface ICartItemService
{
    Task<CartItem?> GetAsync(
        Expression<Func<CartItem, bool>> predicate,
        Func<IQueryable<CartItem>, IIncludableQueryable<CartItem, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CartItem>?> GetListAsync(
        Expression<Func<CartItem, bool>>? predicate = null,
        Func<IQueryable<CartItem>, IOrderedQueryable<CartItem>>? orderBy = null,
        Func<IQueryable<CartItem>, IIncludableQueryable<CartItem, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CartItem> AddAsync(CartItem cartItem);
    Task<CartItem> UpdateAsync(CartItem cartItem);
    Task<CartItem> DeleteAsync(CartItem cartItem, bool permanent = false);
}
