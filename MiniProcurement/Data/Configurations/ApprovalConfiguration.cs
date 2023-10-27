using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations;

public class ApprovalConfiguration : IEntityTypeConfiguration<Approval>
{
    public void Configure(EntityTypeBuilder<Approval> builder)
    {
        builder
            .HasOne(a => a.Document)
            .WithMany(d => d.Approvals)
            .HasForeignKey(a => a.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(a => a.User)
                .WithMany(d => d.Approvals)
                .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}