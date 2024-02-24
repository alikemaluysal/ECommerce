using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BasketRepository : EfRepositoryBase<Basket, Guid, BaseDbContext>, IBasketRepository
{
    public BasketRepository(BaseDbContext context) : base(context)
    {
    }
}