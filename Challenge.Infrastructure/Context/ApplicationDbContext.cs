using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Context {
    public class ApplicationDbContext : DbContext, IApplicationDbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        public async Task<int> SaveChangesAsync() {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PermissionType>(entity => {
                entity.HasIndex(e => e.Description).IsUnique();
            });
            AddDefaultsRecords(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void AddDefaultsRecords(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PermissionType>().HasData(
                new PermissionType { Id = 1, Description = "Admin" },
                new PermissionType { Id = 2, Description = "Creator" },
                new PermissionType { Id = 3, Description = "Editor" },
                new PermissionType { Id = 4, Description = "Viewer" }
            );
        }

    }
}
