using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems").HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id).HasColumnName("Id").IsRequired();
        builder.Property(ci => ci.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(ci => ci.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(ci => ci.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Property(ci => ci.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ci => ci.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ci => ci.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ci => !ci.DeletedDate.HasValue);
    }
}