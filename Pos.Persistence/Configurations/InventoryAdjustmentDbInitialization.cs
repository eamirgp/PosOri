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

            builder.Property(ia => ia.Id)
                .IsRequired();

            builder.Property(ia => ia.WarehouseId)
                .IsRequired();

            builder.Property(ia => ia.AdjustmentTypeId)
                .IsRequired();

            builder.OwnsOne(ia => ia.Date, date =>
            {
                date.Property(d => d.Value)
                    .HasColumnName("Date")
                    .IsRequired();
            });

            builder.OwnsOne(ia => ia.Reason, reason =>
            {
                reason.Property(r => r.Value)
                    .HasColumnName("Reason")
                    .IsRequired()
                    .HasMaxLength(500);
            });

            builder.Property(ia => ia.CreatedDate)
                .IsRequired();

            builder.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(ia => ia.WarehouseId);

            builder.HasOne(ia => ia.AdjustmentType)
                .WithMany()
                .HasForeignKey(ia => ia.AdjustmentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
