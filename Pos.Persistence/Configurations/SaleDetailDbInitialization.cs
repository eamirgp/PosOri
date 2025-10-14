using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class SaleDetailDbInitialization : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(sd => sd.Id);

            builder.Property(sd => sd.Id)
                .IsRequired();

            builder.Property(sd => sd.SaleId)
                .IsRequired();

            builder.Property(sd => sd.ProductId)
                .IsRequired();

            builder.Property(sd => sd.UnitOfMeasureId)
                .IsRequired();

            builder.Property(sd => sd.IGVTypeId)
                .IsRequired();

            builder.OwnsOne(sd => sd.Quantity, quantity =>
            {
                quantity.Property(q => q.Value)
                    .HasColumnName("Quantity")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.OwnsOne(sd => sd.UnitPrice, unitPrice =>
            {
                unitPrice.Property(up => up.Value)
                    .HasColumnName("UnitPrice")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.Property(sd => sd.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(sd => sd.TaxAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(sd => sd.LineTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(sd => sd.CreatedDate)
                .IsRequired();

            builder.HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.SaleId);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(sd => sd.ProductId);

            builder.HasOne<UnitOfMeasure>()
                .WithMany()
                .HasForeignKey(sd => sd.UnitOfMeasureId);

            builder.HasOne<IGVType>()
                .WithMany()
                .HasForeignKey(sd => sd.IGVTypeId);
        }
    }
}
