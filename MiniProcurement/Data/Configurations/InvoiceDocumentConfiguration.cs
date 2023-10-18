using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class InvoiceDocumentConfiguration : IEntityTypeConfiguration<InvoiceDocument>
    {
        public void Configure(EntityTypeBuilder<InvoiceDocument> builder)
        {
            builder.HasKey(inv => inv.DocumentBaseId);

            builder.Property(inv => inv.DocumentBaseId)
                .ValueGeneratedNever();

            builder.Property(inv => inv.PaymentCardNumber).HasMaxLength(16).IsRequired();
        }
    }
}
