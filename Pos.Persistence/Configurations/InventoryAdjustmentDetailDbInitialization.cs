using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class InventoryAdjustmentDetailDbInitialization : IEntityTypeConfiguration<InventoryAdjustmentDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryAdjustmentDetail> builder)
        {
            builder.HasKey(iad => iad.Id);

            builder.Property(iad => iad.Id)
                .IsRequired();

            builder.Property(iad => iad.InventoryAdjustmentId)
                .IsRequired();

            builder.Property(iad => iad.ProductId)
                .IsRequired();

            builder.Property(iad => iad.UnitOfMeasureId)
                .IsRequired();

            builder.OwnsOne(iad => iad.Quantity, quantity =>
            {
                quantity.Property(q => q.Value)
                    .HasColumnName("Quantity")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.Property(iad => iad.Notes)
                .HasMaxLength(500);

            builder.Property(iad => iad.CreatedDate)
                .IsRequired();

            builder.HasOne(iad => iad.InventoryAdjustment)
                .WithMany(ia => ia.Details)
                .HasForeignKey(iad => iad.InventoryAdjustmentId);

            builder.HasOne(iad => iad.Product)
                .WithMany()
                .HasForeignKey(iad => iad.ProductId);
        }
    }
}
