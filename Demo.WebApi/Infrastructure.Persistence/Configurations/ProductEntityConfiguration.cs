using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired()
                .HasColumnName("Id");

            builder
                .Property(x => x.Code).IsRequired()
                .HasColumnType("nvarchar(256)")
                .HasColumnName(nameof(Product.Code))
                .IsRequired();

            builder
                .Property(x => x.DisplayName).IsRequired()
                .HasColumnType("nvarchar(512)")
                .HasColumnName(nameof(Product.DisplayName));

            builder
                .Property(x => x.Description).IsRequired()
                .HasColumnType("nvarchar(1024)")
                .HasColumnName(nameof(Product.Description));

            builder
                .HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
