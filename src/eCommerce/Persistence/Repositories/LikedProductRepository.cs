using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LikedProductRepository : EfRepositoryBase<LikedProduct, Guid, BaseDbContext>, ILikedProductRepository
{
    public LikedProductRepository(BaseDbContext context) : base(context)
    {
    }
}