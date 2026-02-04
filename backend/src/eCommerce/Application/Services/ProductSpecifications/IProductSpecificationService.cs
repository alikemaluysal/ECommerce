using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductSpecifications;

public interface IProductSpecificationService
{
    Task<ProductSpecification?> GetAsync(
        Expression<Func<ProductSpecification, bool>> predicate,
        Func<IQueryable<ProductSpecification>, IIncludableQueryable<ProductSpecification, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductSpecification>?> GetListAsync(
        Expression<Func<ProductSpecification, bool>>? predicate = null,
        Func<IQueryable<ProductSpecification>, IOrderedQueryable<ProductSpecification>>? orderBy = null,
        Func<IQueryable<ProductSpecification>, IIncludableQueryable<ProductSpecification, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductSpecification> AddAsync(ProductSpecification productSpecification);
    Task<ProductSpecification> UpdateAsync(ProductSpecification productSpecification);
    Task<ProductSpecification> DeleteAsync(ProductSpecification productSpecification, bool permanent = false);
}
