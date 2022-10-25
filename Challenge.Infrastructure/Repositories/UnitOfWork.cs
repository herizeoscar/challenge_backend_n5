using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Repositories.Abstractions;

namespace Challenge.Infrastructure.Repositories {
    public class UnitOfWork : IUnitOfWork, IDisposable {

        private ApplicationDbContext context;
        private readonly Dictionary<string, object> repositories = new Dictionary<string, object>();

        public UnitOfWork(ApplicationDbContext context) {
            this.context = context;
        }

        public GenericRepository<T> GetRepository<T>() where T : class {
            string typeName = typeof(T).Name;
            if(repositories.Keys.Contains(typeName)) {
                return repositories[typeName] as GenericRepository<T>;
            }
            GenericRepository<T> newRepository = new GenericRepository<T>(context);

            repositories.Add(typeName, newRepository);
            return newRepository;
        }

        public async Task<int> SaveChangesAsync() {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if(!disposed) {
                if(disposing) {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
