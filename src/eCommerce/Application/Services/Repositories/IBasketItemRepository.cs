using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBasketItemRepository : IAsyncRepository<BasketItem, Guid>, IRepository<BasketItem, Guid>
{
}