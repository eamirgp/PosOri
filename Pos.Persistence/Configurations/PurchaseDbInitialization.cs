using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class PurchaseDbInitialization : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.WarehouseId)
                .IsRequired();

            builder.Property(p => p.VoucherTypeId)
                .IsRequired();

            builder.Property(p => p.CurrencyId)
                .IsRequired();

            builder.Property(p => p.PersonId)
                .IsRequired();

            builder.OwnsOne(p => p.VoucherNumber, voucherNumber =>
            {
                voucherNumber.Property(vn => vn.Serie)
                    .HasColumnName("Serie")
                    .IsRequired()
                    .HasMaxLength(4);

                voucherNumber.Property(vn => vn.Number)
                    .HasColumnName("Number")
                    .IsRequired()
                    .HasMaxLength(8);
            });

            builder.OwnsOne(p => p.IssueDate, issueDate =>
            {
                issueDate.Property(id => id.Value)
                    .HasColumnName("IssueDate")
                    .IsRequired();
            });

            builder.Property(p => p.SubTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Exempt)
                .IsRequired()
                .HasPrecision (18, 2);

            builder.Property(p => p.TaxAmount)
                .IsRequired()
                .HasPrecision (18, 2);

            builder.Property(p => p.Total)
                .IsRequired()
                .HasPrecision (18, 2);

            builder.Property(p => p.CreatedDate)
                .IsRequired();

            builder.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(p => p.WarehouseId);

            builder.HasOne<VoucherType>()
                .WithMany()
                .HasForeignKey(p => p.VoucherTypeId);

            builder.HasOne<Currency>()
                .WithMany()
                .HasForeignKey(p => p.CurrencyId);

            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(p => p.PersonId);
        }
    }
}
