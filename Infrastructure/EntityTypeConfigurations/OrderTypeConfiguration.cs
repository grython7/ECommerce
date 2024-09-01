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
    internal class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o=>o.Id);
            builder.Property(o=>o.Id).HasColumnName("OrderId");

            builder.Property(o=>o.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o=>o.Tax)
                .IsRequired()
                .HasColumnType("float");

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(o => o.UpdatedOn).HasDefaultValueSql("GETDATE()");
        }
    }
}
