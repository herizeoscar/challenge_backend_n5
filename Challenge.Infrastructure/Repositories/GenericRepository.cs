using Challenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Challenge.Infrastructure.Repositories {
    public class GenericRepository<TEntity> where TEntity : class{
        
        internal ApplicationDbContext applicationDbContext;
        internal DbSet<TEntity> dbSet;
        
        public GenericRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
            this.dbSet = applicationDbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "") {
            IQueryable<TEntity> query = dbSet;

            if(filter != null) {
                query = query.Where(filter);
            }

            foreach(var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }

            if(orderBy != null) {
                return await orderBy(query).ToListAsync();
            } else {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetByID(object id) => await dbSet.FindAsync(id);

        public virtual async Task<EntityEntry<TEntity>> Insert(TEntity entity) => await dbSet.AddAsync(entity);

        public virtual void Delete(object id) {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete) {
            if(applicationDbContext.Entry(entityToDelete).State == EntityState.Detached) {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual async Task<EntityEntry<TEntity>> Update(TEntity entityToUpdate) {
            dbSet.Attach(entityToUpdate);
            applicationDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return await Task.FromResult(dbSet.Update(entityToUpdate));
        }
    }
}
