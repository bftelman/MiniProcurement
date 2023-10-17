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
                .HasMany(docbase => docbase.PurchaseRequests)
                .WithOne(prdoc => prdoc.DocumentBase);
            builder
                .HasOne(docbase => docbase.CreatedBy)
                .WithMany(u => u.Documents)
                .HasForeignKey(docbase => docbase.CreatedById);
            builder.Property(docbase => docbase.CreatedOn).HasDefaultValueSql("GETDATE()");
        }
    }
}
