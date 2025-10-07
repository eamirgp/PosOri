using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class InventoryDbInitialization : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired();

            builder.OwnsOne(i => i.Stock, stock =>
            {
                stock.Property(s => s.Value)
                    .HasColumnName("Stock")
                    .IsRequired();
            });

            builder.Property(i => i.CreatedDate)
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(i => i.ProductId);

            builder.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(i => i.WarehouseId);
        }
    }
}
