namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;

    public virtual List<BasketItem> Baskets { get; set; }
    public virtual List<LikedProduct> LikedProducts { get; set; }
    public virtual List<Comment> Comments { get; set; }
    public virtual List<Order> Orders { get; set; }
}
