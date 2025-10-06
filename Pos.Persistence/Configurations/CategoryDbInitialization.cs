using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.Domain.Entities;

namespace Pos.Persistence.Configurations
{
    public class CategoryDbInitialization : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired();

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(50);

                name.HasIndex(n => n.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_Category_Name_Unique");
            });

            builder.OwnsOne(c => c.Description, description =>
            {
                description.Property(d => d.Value)
                    .HasColumnName("Description")
                    .HasMaxLength(100);
            });

            builder.Property(c => c.CreatedDate)
                .IsRequired();
        }
    }
}
