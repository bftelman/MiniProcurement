using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class PurchaseRequestDocumentItemConfiguration : IEntityTypeConfiguration<PurchaseRequestDocumentItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequestDocumentItem> builder)
        {
            builder.Property(pri => pri.Price).IsRequired();
            builder.Property(pri => pri.Quantity).IsRequired();
            builder.Property(pri => pri.MaterialName).IsRequired();
            builder.Property(pri => pri.UnitOfMeasure).IsRequired();
            builder.Property(pri => pri.ItemStatus).IsRequired();
        }
    }
}
