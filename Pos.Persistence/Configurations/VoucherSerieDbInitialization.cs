using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class VoucherSerieDbInitialization : IEntityTypeConfiguration<VoucherSerie>
    {
        public void Configure(EntityTypeBuilder<VoucherSerie> builder)
        {
            builder.HasKey(vs => vs.Id);

            builder.Property(vs => vs.Id)
                .IsRequired();

            builder.Property(vs => vs.VoucherTypeId)
                .IsRequired();

            builder.Property(vs => vs.Serie)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(vs => vs.CurrentNumber)
                .IsRequired();

            builder.Property(vs => vs.CreatedDate)
                .IsRequired();

            builder.HasOne<VoucherType>()
                .WithMany()
                .HasForeignKey(vs => vs.VoucherTypeId);

            builder.HasData(new
            {
                Id = Guid.Parse("6a661525-7402-470d-9251-d14d1cc105cf"),
                VoucherTypeId = Guid.Parse("85B8D448-A864-4F46-8FF1-4DEC57D30D23"),
                Serie = "B001",
                CurrentNumber = 0,
                CreatedDate = new DateTime(2025, 01, 01)
            },
            new
            {
                Id = Guid.Parse("323dedd6-1dbe-4958-bcec-470c1e6adbd9"),
                VoucherTypeId = Guid.Parse("9E991AEC-8D56-438E-966F-E5D8079D2AB7"),
                Serie = "F001",
                CurrentNumber = 0,
                CreatedDate = new DateTime(2025, 01, 01)
            });
        }
    }
}
