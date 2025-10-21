using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class PurchaseDetailDbInitialization : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.HasKey(pd => pd.Id);

            builder.Property(pd => pd.Id)
                .IsRequired();

            builder.Property(pd => pd.PurchaseId)
                .IsRequired();

            builder.Property(pd => pd.ProductId)
                .IsRequired();

            builder.Property(pd => pd.UnitOfMeasureId)
                .IsRequired();

            builder.Property(pd => pd.IGVTypeId)
                .IsRequired();

            builder.OwnsOne(pd => pd.Quantity, quantity =>
            {
                quantity.Property(q => q.Value)
                    .HasColumnName("Quantity")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.OwnsOne(pd => pd.UnitValue, unitValue =>
            {
                unitValue.Property(uv => uv.Value)
                    .HasColumnName("UnitValue")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.Property(pd => pd.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(pd => pd.TaxAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(pd => pd.LineTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasOne(pd => pd.Purchase)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.PurchaseId);

            builder.HasOne(pd => pd.Product)
                .WithMany()
                .HasForeignKey(pd => pd.ProductId);

            builder.HasOne<UnitOfMeasure>()
                .WithMany()
                .HasForeignKey(pd => pd.UnitOfMeasureId);

            builder.HasOne(pd => pd.IGVType)
                .WithMany()
                .HasForeignKey(pd => pd.IGVTypeId);
        }
    }
}
