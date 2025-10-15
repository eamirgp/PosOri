using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class IGVTypeDbInitialization : IEntityTypeConfiguration<IGVType>
    {
        public void Configure(EntityTypeBuilder<IGVType> builder)
        {
            builder.HasKey(it => it.Id);

            builder.Property(it => it.Id)
                .IsRequired();

            builder.Property(it => it.Code)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(it => it.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(it => it.Rate)
                .IsRequired()
                .HasPrecision(3, 2);

            builder.Property(it => it.CreatedDate)
                .IsRequired();

            builder.HasData(new
            {
                Id = Guid.Parse("87f49009-4041-4a62-953a-27a303286e89"),
                Code = "10",
                Description = "Gravado - Operación onerosa",
                Rate = 0.18m,
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("aa5e5c9d-1912-456c-854b-1b3d80ac2e9b"),
                Code = "30",
                Description = "Inafecto - Operación onerosa",
                Rate = 0.00m,
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
