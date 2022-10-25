using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Context {
    public interface IApplicationDbContext {

        DbSet<Permission> Permissions { get; set; }

        DbSet<PermissionType> PermissionTypes { get; set; }

        Task<int> SaveChangesAsync();

    }
}
