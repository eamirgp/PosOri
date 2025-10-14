using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class SaleDbInitialization : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.WarehouseId)
                .IsRequired();

            builder.Property(s => s.VoucherTypeId)
                .IsRequired();

            builder.Property(s => s.CurrencyId)
                .IsRequired();

            builder.Property(s => s.PersonId)
                .IsRequired();

            builder.OwnsOne(s => s.VoucherNumber, voucherNumber =>
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

            builder.OwnsOne(s => s.IssueDate, issueDate =>
            {
                issueDate.Property(id => id.Value)
                    .HasColumnName("IssueDate")
                    .IsRequired();
            });

            builder.Property(s => s.SubTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.TaxAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.Total)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.CreatedDate)
                .IsRequired();

            builder.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(s => s.WarehouseId);

            builder.HasOne<VoucherType>()
                .WithMany()
                .HasForeignKey(s => s.VoucherTypeId);

            builder.HasOne<Currency>()
                .WithMany()
                .HasForeignKey(s => s.CurrencyId);

            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(s => s.PersonId);

            builder.HasIndex(s => new {
                    s.VoucherNumber.Serie,
                    s.VoucherNumber.Number,
                    s.VoucherTypeId
                })
                .IsUnique()
                .HasDatabaseName("IX_Sale_VoucherNumber_Unique");
        }
    }
}
