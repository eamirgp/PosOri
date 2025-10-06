using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class WarehouseDbInitialization : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .IsRequired();

            builder.OwnsOne(w => w.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(50);

                name.HasIndex(n => n.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_Warehouse_Name_Unique");
            });

            builder.OwnsOne(w => w.Address, address =>
            {
                address.Property(a => a.Value)
                    .HasColumnName("Address")
                    .HasMaxLength(200);
            });

            builder.Property(w => w.CreatedDate)
                .IsRequired();
        }
    }
}
