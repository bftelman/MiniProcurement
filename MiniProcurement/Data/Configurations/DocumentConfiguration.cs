using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.Property(docbase => docbase.DocumentNumber)
            .IsRequired();
        
        builder
            .HasOne(docbase => docbase.PurchaseRequest)
            .WithOne(prdoc => prdoc.Document)
            .HasForeignKey<PurchaseRequest>(prdoc => prdoc.DocumentId);

        builder
            .HasOne(docbase => docbase.InvoiceRequest)
            .WithOne(invoice => invoice.Document)
            .HasForeignKey<InvoiceRequest>(invoice => invoice.DocumentId);

        builder
            .HasOne(docbase => docbase.CreatedBy)
            .WithMany(u => u.Documents)
            .HasForeignKey(docbase => docbase.CreatedById);

        builder.HasMany(docbase => docbase.Approvals)
            .WithOne(u => u.Document)
            .HasForeignKey(u => u.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(docbase => docbase.CreatedOn)
            .HasDefaultValueSql("GETDATE()");
    }
}