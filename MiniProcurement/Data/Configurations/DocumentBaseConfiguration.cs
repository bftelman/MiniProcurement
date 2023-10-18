using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class DocumentBaseConfiguration : IEntityTypeConfiguration<DocumentBase>
    {
        public void Configure(EntityTypeBuilder<DocumentBase> builder)
        {
            builder.Property(docbase => docbase.DocumentNumber).IsRequired();
            builder
                .HasOne(docbase => docbase.PurchaseRequest)
                .WithOne(prdoc => prdoc.DocumentBase)
                .HasForeignKey<PurchaseRequestDocument>(prdoc => prdoc.DocumentBaseId);

            builder
                .HasOne(docbase => docbase.InvoiceRequest)
                .WithOne(invoice => invoice.DocumentBase)
                .HasForeignKey<InvoiceDocument>(invoice => invoice.DocumentBaseId);


            builder
                .HasOne(docbase => docbase.CreatedBy)
                .WithMany(u => u.Documents)
                .HasForeignKey(docbase => docbase.CreatedById);
            builder.Property(docbase => docbase.CreatedOn).HasDefaultValueSql("GETDATE()");
        }
    }
}
