using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class InventoryMovementDbInitialization : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.HasIndex(im => im.Id);

            builder.Property(im => im.ProductId)
                .IsRequired();

            builder.Property(im => im.WarehouseId)
                .IsRequired();

            builder.OwnsOne(im => im.MovementType, movementType =>
            {
                movementType.Property(mt => mt.Value)
                    .HasColumnName("MovementType")
                    .IsRequired()
                    .HasMaxLength(20);
            });

            builder.OwnsOne(im => im.Quantity, quantity =>
            {
                quantity.Property(q => q.Value)
                    .HasColumnName("Quantity")
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            builder.Property(im => im.ReferenceId);

            builder.Property(im => im.PreviousStock)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(im => im.NewStock)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(im => im.CreatedDate)
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(im => im.ProductId);

            builder.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(im => im.WarehouseId);
        }
    }
}
