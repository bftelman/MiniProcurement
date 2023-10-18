using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class PurchaseRequestDocumentConfiguration : IEntityTypeConfiguration<PurchaseRequestDocument>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequestDocument> builder)
        {
            builder.HasKey(prdoc => prdoc.DocumentBaseId);

            builder.Property(prdoc => prdoc.DocumentBaseId)
                .ValueGeneratedNever();

            builder.Property(prdoc => prdoc.DeliveryAddress).IsRequired();
            builder
                .HasMany(prdoc => prdoc.PurchaseRequestDocumentItems)
                .WithOne(prdocitem => prdocitem.PurchaseRequestDocument)
                .HasForeignKey(prdoc => prdoc.PurchaseRequestDocumentId);
        }
    }
}
