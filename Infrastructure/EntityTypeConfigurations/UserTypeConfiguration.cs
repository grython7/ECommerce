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
            builder.HasData(
                new User
                {
                    Id = new Guid("3ab4b46a-8d10-41ce-d001-08dccb73f215"),
                    Name = "admin",
                    Address = "123 bla bla st.",
                    Phone = "01234567890",
                    Email = "admin@ldc.com",
                    PasswordSalt = "xLLO/PUZthdu0xjc/YNY1w==",
                    PasswordHash = "Cvpqm17FCPsRjAdKdJTitnyNu2isY88GFVMfOLDiLKA=",
                    Status = "Active",
                    IsAdmin = true,
                    CreatedOn = DateTime.Parse("01/09/2024 02:14:54"),
                    UpdatedOn = DateTime.Parse("01/09/2024 02:14:54"),
                    IsDeleted = false
                }
            );

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
