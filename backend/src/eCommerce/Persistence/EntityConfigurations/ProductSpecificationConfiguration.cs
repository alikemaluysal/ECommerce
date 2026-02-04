using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.ToTable("ProductSpecifications").HasKey(ps => ps.Id);

        builder.Property(ps => ps.Id).HasColumnName("Id").IsRequired();
        builder.Property(ps => ps.Key).HasColumnName("Key").IsRequired();
        builder.Property(ps => ps.Value).HasColumnName("Value").IsRequired();
        builder.Property(ps => ps.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(ps => ps.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ps => ps.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ps => ps.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ps => !ps.DeletedDate.HasValue);
    }
}