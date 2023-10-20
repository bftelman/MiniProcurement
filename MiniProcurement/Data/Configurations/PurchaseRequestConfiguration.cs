using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class PurchaseRequestConfiguration : IEntityTypeConfiguration<PurchaseRequest>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
        {
            builder.HasKey(prdoc => prdoc.DocumentId);

            builder.Property(prdoc => prdoc.DocumentId)
                .ValueGeneratedNever();

            builder.Property(prdoc => prdoc.DeliveryAddress).IsRequired();
            builder
                .HasMany(prdoc => prdoc.PurchaseRequestItems)
                .WithOne(prdocitem => prdocitem.PurchaseRequest)
                .HasForeignKey(prdoc => prdoc.PurchaseRequestId);
        }
    }
}
