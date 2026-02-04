using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CartItemRepository : EfRepositoryBase<CartItem, Guid, BaseDbContext>, ICartItemRepository
{
    public CartItemRepository(BaseDbContext context) : base(context)
    {
    }
}