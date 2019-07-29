using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductCategoryEntityConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired()
                .HasColumnName("Id");

            builder
                .Property(x => x.Name).IsRequired()
                .HasColumnType("nvarchar(256)")
                .HasColumnName(nameof(ProductCategory.Name))
                .IsRequired();

            builder
                .Property(x => x.DisplayName).IsRequired()
                .HasColumnType("nvarchar(512)")
                .HasColumnName(nameof(ProductCategory.DisplayName));

        }
    }
}
