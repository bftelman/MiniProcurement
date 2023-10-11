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
        public DbSet<UserRole> UserRoles { get; set; }

        #region Entity configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
            new UserRoleConfiguration().Configure(modelBuilder.Entity<UserRole>());
            new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());
        }
        #endregion
    }
}
