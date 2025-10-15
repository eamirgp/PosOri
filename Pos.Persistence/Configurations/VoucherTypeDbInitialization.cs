using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class VoucherTypeDbInitialization : IEntityTypeConfiguration<VoucherType>
    {
        public void Configure(EntityTypeBuilder<VoucherType> builder)
        {
            builder.HasKey(vt => vt.Id);

            builder.Property(vt => vt.Id)
                .IsRequired();

            builder.Property(vt => vt.Code)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(vt => vt.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(vt => vt.CreatedDate)
                .IsRequired();

            builder.HasData(new
            {
                Id = Guid.Parse("9e991aec-8d56-438e-966f-e5d8079d2ab7"),
                Code = "01",
                Description = "Factura",
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("85b8d448-a864-4f46-8ff1-4dec57d30d23"),
                Code = "03",
                Description = "Boleta de venta",
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
