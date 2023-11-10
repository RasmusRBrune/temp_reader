using System.Linq.Expressions;

namespace temperature_Server.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        public Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null);
        //public Task<bool> CheckIfExistsAsync(T entity);
        public Task<T> DeleteAsync(Expression<Func<T, bool>> expression);
        public Task<T> UpdateAsync(T entity);
        public Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);
        public Task<T> AddAsync(T entity);
        //public Task<T> AddAsync(IEnumerable<T> entities);
        public Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        public abstract Task<T> GetSingleWithIncludeAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        public abstract Task<IEnumerable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> expression = null);
    }
}
