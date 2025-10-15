using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class DocumentTypeDbInitialization : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasKey(dt => dt.Id);

            builder.Property(dt => dt.Id)
                .IsRequired();

            builder.Property(dt => dt.Code)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(dt => dt.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(dt => dt.Length)
                .IsRequired();

            builder.Property(dt => dt.CreatedDate)
                .IsRequired();

            builder.HasData(new
            {
                Id = Guid.Parse("2a5c854a-cb59-4a18-9c30-ba30b045af0f"),
                Code = "1",
                Description = "D.N.I.",
                Length = 8,
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("48b2bc94-18da-4c63-b577-ebd242448600"),
                Code = "6",
                Description = "R.U.C.",
                Length = 11,
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
