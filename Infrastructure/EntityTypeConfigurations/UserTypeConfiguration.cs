using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfigurations
{
    internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("UserId");

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Address).IsRequired();

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(u => u.Email).IsRequired();

            builder.Property(u => u.PasswordSalt).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();

            builder.Property(u => u.Status).IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("CK_User_Status", "Status IN ('active', 'inactive')"));


            builder.Property(u => u.IsAdmin)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.UpdatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
