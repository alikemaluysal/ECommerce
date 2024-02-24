using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages").HasKey(pi => pi.Id);

        builder.Property(pi => pi.Id).HasColumnName("Id").IsRequired();
        builder.Property(pi => pi.ProductId).HasColumnName("ProductId");
        builder.Property(pi => pi.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(pi => pi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pi => pi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pi => pi.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pi => !pi.DeletedDate.HasValue);
    }
}