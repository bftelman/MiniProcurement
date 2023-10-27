using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations;

public class PurchaseRequestItemConfiguration : IEntityTypeConfiguration<PurchaseRequestItem>
{
    public void Configure(EntityTypeBuilder<PurchaseRequestItem> builder)
    {
        builder.Property(pri => pri.Price).IsRequired();
        builder.Property(pri => pri.Quantity).IsRequired();
        builder.Property(pri => pri.MaterialName).IsRequired();
        builder.Property(pri => pri.UnitOfMeasure).IsRequired();
        builder.Property(pri => pri.ItemStatus).IsRequired();
    }
}