using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class InventoryAdjustmentDbInitialization : IEntityTypeConfiguration<InventoryAdjustment>
    {
        public void Configure(EntityTypeBuilder<InventoryAdjustment> builder)
        {
            builder.HasKey(ia => ia.Id);

            builder.Property(ia => ia.WarehouseId)
                .IsRequired();

            builder.Property(ia => ia.InventoryAdjustmentTypeId)
                .IsRequired();

            builder.OwnsOne(ia => ia.Reason, reason =>
            {
                reason.Property(r => r.Value)
                    .HasColumnName("Reason")
                    .IsRequired()
                    .HasMaxLength(200);
            });

            builder.Property(ia => ia.CreatedDate)
                .IsRequired();

            builder.HasMany(ia => ia.InventoryAdjustmentDetails)
                .WithOne()
                .HasForeignKey(iad => iad.InventoryAdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<InventoryAdjustmentType>()
                .WithMany()
                .HasForeignKey(ia => ia.InventoryAdjustmentTypeId);

            builder.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(ia => ia.WarehouseId);
        }
    }
}
