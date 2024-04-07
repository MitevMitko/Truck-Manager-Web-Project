namespace TruckManagerSoftware.Infrastructure.Repository.Contract
{
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        Task<T> GetByString(string name);

        Task<IEnumerable<T>> GetAll();

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
