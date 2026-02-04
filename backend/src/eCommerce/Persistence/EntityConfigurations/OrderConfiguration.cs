using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);

        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o => o.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(o => o.TotalAmount).HasColumnName("TotalAmount").IsRequired();
        builder.Property(o => o.Status).HasColumnName("Status").IsRequired();
        builder.Property(o => o.ShippingAddress).HasColumnName("ShippingAddress").IsRequired();
        builder.Property(o => o.ShippingCity).HasColumnName("ShippingCity").IsRequired();
        builder.Property(o => o.ShippingCountry).HasColumnName("ShippingCountry").IsRequired();
        builder.Property(o => o.ShippingPostalCode).HasColumnName("ShippingPostalCode").IsRequired();
        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);
    }
}