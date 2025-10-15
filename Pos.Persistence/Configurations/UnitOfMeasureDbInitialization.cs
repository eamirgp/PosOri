using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class UnitOfMeasureDbInitialization : IEntityTypeConfiguration<UnitOfMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.HasKey(um => um.Id);

            builder.Property(um => um.Id)
                .IsRequired();

            builder.Property(um => um.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(um => um.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(um => um.CreatedDate)
                .IsRequired();

            builder.HasData(new
            {
                Id = Guid.Parse("cc5cfa04-65b3-4cb2-9cd4-40c1a6c98b73"),
                Code = "BX",
                Description = "Caja",
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("e6dff931-a8aa-4c6f-9e5f-e784c1af1c08"),
                Code = "DZN",
                Description = "Docena",
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("f6c0baf5-f900-41d9-a1dd-ede1d927c2e2"),
                Code = "NIU",
                Description = "Unidad",
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
