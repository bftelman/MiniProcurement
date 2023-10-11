using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(u => u.Department)
               .WithMany()
               .HasForeignKey(u => u.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
