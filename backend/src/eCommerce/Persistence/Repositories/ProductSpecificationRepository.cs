using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductSpecificationRepository : EfRepositoryBase<ProductSpecification, Guid, BaseDbContext>, IProductSpecificationRepository
{
    public ProductSpecificationRepository(BaseDbContext context) : base(context)
    {
    }
}