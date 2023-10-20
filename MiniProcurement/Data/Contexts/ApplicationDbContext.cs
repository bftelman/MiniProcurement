using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Configurations;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<InvoiceRequest> InvoiceRequests { get; set; }
        public DbSet<PurchaseRequestItem> PurchaseRequestItems { get; set; }
        public DbSet<InvoiceRequestItem> InvoiceRequestItems { get; set; }

        #region Entity configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
            new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());
            new DocumentConfiguration().Configure(modelBuilder.Entity<Document>());
            new InvoiceRequestConfiguration().Configure(modelBuilder.Entity<InvoiceRequest>());
            new PurchaseRequestConfiguration().Configure(modelBuilder.Entity<PurchaseRequest>());
            new PurchaseRequestItemConfiguration().Configure(modelBuilder.Entity<PurchaseRequestItem>());
        }
        #endregion
    }
}
