using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserEntityConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasDefaultValueSql("newsequentialid()")
                .HasColumnName("Id")
                .IsRequired();

            builder
                .Property(x => x.UserName).IsRequired()
                .HasColumnType("nvarchar(256)")
                .HasColumnName(nameof(User.UserName))
                .IsRequired();

            builder
                .Property(x => x.DisplayName)
                .HasColumnType("nvarchar(512)")
                .HasColumnName(nameof(User.DisplayName))
                .IsRequired();
        }
    }
}
