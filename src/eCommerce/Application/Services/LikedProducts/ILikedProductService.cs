using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LikedProducts;

public interface ILikedProductService
{
    Task<LikedProduct?> GetAsync(
        Expression<Func<LikedProduct, bool>> predicate,
        Func<IQueryable<LikedProduct>, IIncludableQueryable<LikedProduct, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LikedProduct>?> GetListAsync(
        Expression<Func<LikedProduct, bool>>? predicate = null,
        Func<IQueryable<LikedProduct>, IOrderedQueryable<LikedProduct>>? orderBy = null,
        Func<IQueryable<LikedProduct>, IIncludableQueryable<LikedProduct, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LikedProduct> AddAsync(LikedProduct likedProduct);
    Task<LikedProduct> UpdateAsync(LikedProduct likedProduct);
    Task<LikedProduct> DeleteAsync(LikedProduct likedProduct, bool permanent = false);
}
