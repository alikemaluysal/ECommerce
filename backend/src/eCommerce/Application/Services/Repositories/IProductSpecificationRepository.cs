using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProductSpecificationRepository : IAsyncRepository<ProductSpecification, Guid>, IRepository<ProductSpecification, Guid>
{
}