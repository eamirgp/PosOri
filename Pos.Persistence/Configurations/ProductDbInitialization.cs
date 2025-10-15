using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class ProductDbInitialization : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.OwnsOne(p => p.Code, code =>
            {
                code.Property(c => c.Value)
                    .HasColumnName("Code")
                    .IsRequired()
                    .HasMaxLength(20);
            });

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.OwnsOne(p => p.Description, description =>
            {
                description.Property(d => d.Value)
                    .HasColumnName("Description")
                    .HasMaxLength(200);
            });

            builder.OwnsOne(p => p.PurchasePrice, purchasePrice =>
            {
                purchasePrice.Property(pp => pp.Value)
                    .HasColumnName("PurchasePrice")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.OwnsOne(p => p.SalePrice, salePrice =>
            {
                salePrice.Property(sp => sp.Value)
                    .HasColumnName("SalePrice")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.Property(p => p.CreatedDate)
                .IsRequired();

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
