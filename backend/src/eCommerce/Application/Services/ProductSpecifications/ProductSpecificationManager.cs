using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductSpecifications;

public class ProductSpecificationManager : IProductSpecificationService
{
    private readonly IProductSpecificationRepository _productSpecificationRepository;

    public ProductSpecificationManager(IProductSpecificationRepository productSpecificationRepository)
    {
        _productSpecificationRepository = productSpecificationRepository;
    }

    public async Task<ProductSpecification?> GetAsync(
        Expression<Func<ProductSpecification, bool>> predicate,
        Func<IQueryable<ProductSpecification>, IIncludableQueryable<ProductSpecification, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductSpecification? productSpecification = await _productSpecificationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productSpecification;
    }

    public async Task<IPaginate<ProductSpecification>?> GetListAsync(
        Expression<Func<ProductSpecification, bool>>? predicate = null,
        Func<IQueryable<ProductSpecification>, IOrderedQueryable<ProductSpecification>>? orderBy = null,
        Func<IQueryable<ProductSpecification>, IIncludableQueryable<ProductSpecification, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductSpecification> productSpecificationList = await _productSpecificationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productSpecificationList;
    }

    public async Task<ProductSpecification> AddAsync(ProductSpecification productSpecification)
    {
        ProductSpecification addedProductSpecification = await _productSpecificationRepository.AddAsync(productSpecification);

        return addedProductSpecification;
    }

    public async Task<ProductSpecification> UpdateAsync(ProductSpecification productSpecification)
    {
        ProductSpecification updatedProductSpecification = await _productSpecificationRepository.UpdateAsync(productSpecification);

        return updatedProductSpecification;
    }

    public async Task<ProductSpecification> DeleteAsync(ProductSpecification productSpecification, bool permanent = false)
    {
        ProductSpecification deletedProductSpecification = await _productSpecificationRepository.DeleteAsync(productSpecification);

        return deletedProductSpecification;
    }
}
