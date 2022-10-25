namespace Challenge.Infrastructure.Repositories.Abstractions {
    public interface IUnitOfWork {

        GenericRepository<T> GetRepository<T>() where T : class;

        Task<int> SaveChangesAsync();

    }
}
