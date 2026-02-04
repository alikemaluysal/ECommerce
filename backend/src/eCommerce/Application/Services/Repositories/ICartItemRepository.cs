using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICartItemRepository : IAsyncRepository<CartItem, Guid>, IRepository<CartItem, Guid>
{
}