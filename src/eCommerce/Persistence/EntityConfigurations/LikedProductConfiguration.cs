using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LikedProductConfiguration : IEntityTypeConfiguration<LikedProduct>
{
    public void Configure(EntityTypeBuilder<LikedProduct> builder)
    {
        builder.ToTable("LikedProducts").HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id).HasColumnName("Id").IsRequired();
        builder.Property(lp => lp.UserId).HasColumnName("UserId");
        builder.Property(lp => lp.ProductId).HasColumnName("ProductId");
        builder.Property(lp => lp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lp => lp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lp => lp.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lp => !lp.DeletedDate.HasValue);
    }
}