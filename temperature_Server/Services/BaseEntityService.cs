

using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public abstract class BaseEntityService<T, TId> : IService<T, TId>
        where T : class, IEntity<TId>, new()
    {
        protected readonly IRepository<T> _repository;

        public BaseEntityService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await _repository.AddAsync(entity);
            return result;
        }

        public virtual async Task<T> DeleteAsync(TId id)
        {
            var result = await _repository.DeleteAsync(e => e.Id.ToString() == id.ToString());
            return result;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var result = await _repository.UpdateAsync(entity);
            return result;
        }

        public virtual async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            var result = await _repository.UpdateAsync(entities);
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync()
        {
            var result = await _repository.GetAllWithIncludeAsync();
            return result;
        }

        public async Task<int> GetCountAsync()
        {
            var result = await _repository.GetCountAsync();
            return result;
        }

        public async Task<T> GetSingleAsync(TId id)
        {
            var result = await _repository.GetSingleAsync(e => e.Id.ToString() == id.ToString());
            return result;
        }

        public async Task<T> GetSingleWithIncludeAsync(TId id)
        {
            var result = await _repository.GetSingleWithIncludeAsync(e => e.Id.ToString() == id.ToString());
            return result;
        }

    }
}