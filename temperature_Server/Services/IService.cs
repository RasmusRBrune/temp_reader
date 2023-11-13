namespace temperature_Server.Services
{
    public interface IService<T, IdType> where T : class
    {
        public Task<T> DeleteAsync(IdType id);
        public Task<T> UpdateAsync(T entity);
        public Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);
        public Task<T> AddAsync(T entity);
        public Task<T> GetSingleAsync(IdType id);
        public Task<T> GetSingleWithIncludeAsync(IdType id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllWithIncludeAsync();
        public Task<int> GetCountAsync();
    }
}
