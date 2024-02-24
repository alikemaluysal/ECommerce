using Application.Features.LikedProducts.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LikedProducts;

public class LikedProductManager : ILikedProductService
{
    private readonly ILikedProductRepository _likedProductRepository;
    private readonly LikedProductBusinessRules _likedProductBusinessRules;

    public LikedProductManager(ILikedProductRepository likedProductRepository, LikedProductBusinessRules likedProductBusinessRules)
    {
        _likedProductRepository = likedProductRepository;
        _likedProductBusinessRules = likedProductBusinessRules;
    }

    public async Task<LikedProduct?> GetAsync(
        Expression<Func<LikedProduct, bool>> predicate,
        Func<IQueryable<LikedProduct>, IIncludableQueryable<LikedProduct, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LikedProduct? likedProduct = await _likedProductRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return likedProduct;
    }

    public async Task<IPaginate<LikedProduct>?> GetListAsync(
        Expression<Func<LikedProduct, bool>>? predicate = null,
        Func<IQueryable<LikedProduct>, IOrderedQueryable<LikedProduct>>? orderBy = null,
        Func<IQueryable<LikedProduct>, IIncludableQueryable<LikedProduct, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LikedProduct> likedProductList = await _likedProductRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return likedProductList;
    }

    public async Task<LikedProduct> AddAsync(LikedProduct likedProduct)
    {
        LikedProduct addedLikedProduct = await _likedProductRepository.AddAsync(likedProduct);

        return addedLikedProduct;
    }

    public async Task<LikedProduct> UpdateAsync(LikedProduct likedProduct)
    {
        LikedProduct updatedLikedProduct = await _likedProductRepository.UpdateAsync(likedProduct);

        return updatedLikedProduct;
    }

    public async Task<LikedProduct> DeleteAsync(LikedProduct likedProduct, bool permanent = false)
    {
        LikedProduct deletedLikedProduct = await _likedProductRepository.DeleteAsync(likedProduct);

        return deletedLikedProduct;
    }
}
