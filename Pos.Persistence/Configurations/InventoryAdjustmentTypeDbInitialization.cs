using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class InventoryAdjustmentTypeDbInitialization : IEntityTypeConfiguration<InventoryAdjustmentType>
    {
        public void Configure(EntityTypeBuilder<InventoryAdjustmentType> builder)
        {
            builder.HasKey(iat => iat.Id);

            builder.Property(iat => iat.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(iat => iat.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(iat => iat.CreatedDate)
                .IsRequired();

            builder.HasData(
            new
            {
                Id = Guid.Parse("ca1727a8-9229-408a-9b5b-c72670e1a4eb"),
                Code = "INC",
                Description = "Aumentar",
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("294b166f-b224-44c9-aa65-db7520ed127c"),
                Code = "DEC",
                Description = "Disminuir",
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
