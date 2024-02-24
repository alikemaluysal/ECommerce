using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Comment : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }

    public virtual User User { get; set; }
    public virtual Product Product { get; set; }


}
