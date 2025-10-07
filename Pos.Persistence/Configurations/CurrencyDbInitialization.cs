using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class CurrencyDbInitialization : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.CreatedDate)
                .IsRequired();

            builder.HasIndex(c => c.Code)
                .IsUnique()
                .HasDatabaseName("IX_Currency_Code_Unique");

            builder.HasData(new
            {
                Id = Guid.Parse("e5228390-2c0b-434d-8fe1-886ab507ca98"),
                Code = "PEN",
                Description = "Soles",
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("df74185a-0502-418f-b393-364ea33d1aed"),
                Code = "USD",
                Description = "Dólares",
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
