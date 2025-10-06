using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class PersonDbInitialization : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.OwnsOne(p => p.DocumentNumber, documentNumber =>
            {
                documentNumber.Property(dn => dn.Value)
                    .HasColumnName("DocumentNumber")
                    .IsRequired()
                    .HasMaxLength(11);

                documentNumber.HasIndex(dn => dn.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_Person_DocumentNumber_Unique");
            });

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(150);
            });

            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Value)
                    .HasColumnName("Address")
                    .HasMaxLength(200);
            });

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(100);
            });

            builder.OwnsOne(p => p.Phone, phone =>
            {
                phone.Property(p => p.Value)
                    .HasColumnName("Phone")
                    .HasMaxLength(20);
            });

            builder.Property(p => p.CreatedDate)
                .IsRequired();

            builder.HasOne(p => p.DocumentType)
                .WithMany()
                .HasForeignKey(p => p.DocumentTypeId);
        }
    }
}
