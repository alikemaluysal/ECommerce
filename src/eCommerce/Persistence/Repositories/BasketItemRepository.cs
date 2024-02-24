using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BasketItemRepository : EfRepositoryBase<BasketItem, Guid, BaseDbContext>, IBasketItemRepository
{
    public BasketItemRepository(BaseDbContext context) : base(context)
    {
    }
}