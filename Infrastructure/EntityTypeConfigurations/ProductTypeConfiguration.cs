using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfigurations
{
    internal class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ProductId");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                //.IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.Property(p => p.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.UpdatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            //builder.Property(p => p.Quantity).IsRequired();
        }
    }
}
