using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Category : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

}
