using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations;

public class InvoiceRequestItemConfiguration : IEntityTypeConfiguration<InvoiceRequestItem>
{
    public void Configure(EntityTypeBuilder<InvoiceRequestItem> builder)
    {
        builder.HasOne(inv => inv.PurchaseRequestItem)
            .WithMany(pr => pr.InvoiceRequestItems)
            .HasForeignKey(inv => inv.PurchaseRequestItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(item => item.InvoiceRequest)
            .WithMany(inv => inv.InvoiceRequestItems)
            .HasForeignKey(item => item.InvoiceRequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}