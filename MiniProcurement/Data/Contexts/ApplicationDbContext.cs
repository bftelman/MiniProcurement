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
        public DbSet<DocumentBase> Documents { get; set; }
        public DbSet<PurchaseRequestDocument> PurchaseRequests { get; set; }
        public DbSet<InvoiceDocument> Invoices { get; set; }
        public DbSet<PurchaseRequestDocumentItem> PurchaseRequestDocumentItems { get; set; }

        #region Entity configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
            new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());
            new DocumentBaseConfiguration().Configure(modelBuilder.Entity<DocumentBase>());
            new InvoiceDocumentConfiguration().Configure(modelBuilder.Entity<InvoiceDocument>());
            new PurchaseRequestDocumentConfiguration().Configure(modelBuilder.Entity<PurchaseRequestDocument>());
            new PurchaseRequestDocumentItemConfiguration().Configure(modelBuilder.Entity<PurchaseRequestDocumentItem>());
        }
        #endregion
    }
}
