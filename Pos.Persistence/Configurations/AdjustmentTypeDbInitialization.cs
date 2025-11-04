using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class AdjustmentTypeDbInitialization : IEntityTypeConfiguration<AdjustmentType>
    {
        public void Configure(EntityTypeBuilder<AdjustmentType> builder)
        {
            builder.HasKey(at => at.Id);

            builder.Property(at => at.Id)
                .IsRequired();

            builder.Property(at => at.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(at => at.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(at => at.CreatedDate)
                .IsRequired();

            // Seed data
            builder.HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Code = "INITIAL",
                    Description = "Inventario Inicial",
                    CreatedDate = new DateTime(2025, 01, 01)
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Code = "POSITIVE",
                    Description = "Ajuste Positivo",
                    CreatedDate = new DateTime(2025, 01, 01)
                },
                new
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Code = "NEGATIVE",
                    Description = "Ajuste Negativo",
                    CreatedDate = new DateTime(2025, 01, 01)
                }
            );
        }
    }
}
