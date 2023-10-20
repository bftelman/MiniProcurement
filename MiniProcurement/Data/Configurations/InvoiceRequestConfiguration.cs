using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class InvoiceRequestConfiguration : IEntityTypeConfiguration<InvoiceRequest>
    {
        public void Configure(EntityTypeBuilder<InvoiceRequest> builder)
        {
            builder.HasKey(inv => inv.DocumentId);

            builder.Property(inv => inv.DocumentId)
                .ValueGeneratedNever();

            builder.Property(inv => inv.PaymentCardNumber).HasMaxLength(16).IsRequired();
        }
    }
}
