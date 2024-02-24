using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class LikedProduct : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}
