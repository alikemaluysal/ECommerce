using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILikedProductRepository : IAsyncRepository<LikedProduct, Guid>, IRepository<LikedProduct, Guid>
{
}