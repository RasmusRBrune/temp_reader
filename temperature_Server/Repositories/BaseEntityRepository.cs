using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using temperature_Server.Data;
using temperature_Server.Data.Context;
using temperature_Server.Extensions;

namespace temperature_Server.Repositories
{
    public abstract class BaseEntityRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected readonly TempReaderContext _context;

        public BaseEntityRepository(TempReaderContext context)
        {
            _context = context;
        }
        protected IQueryable<T> QueryAll()
        {
            try
            {
                return _context.Set<T>();//.AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} {typeof(T)} must not be null");
            }

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }


        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} {typeof(T)} must not be null");
            }
            try
            {
                var local = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == entity.Id);
                // _context.Update(entity);

                _context.Entry(local).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(T)} could not be updated: {ex.Message}");
            }
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} ienumerable of {typeof(T)} must not be null");
            }
            try
            {
                foreach (var entity in entities)
                {
                    var local = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == entity.Id);
                    _context.Entry(local).CurrentValues.SetValues(entity);
                }

                await _context.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"ienumerable of {typeof(T)} could not be updated: {ex.Message}");
            }
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            try
            {
                return await QueryAll().WhereIf(expression).ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public abstract Task<IEnumerable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> expression = null);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await QueryAll().FirstOrDefaultAsync(expression);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public abstract Task<T> GetSingleWithIncludeAsync(Expression<Func<T, bool>> expression);

        public async Task<T> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteAsync)} expression must not be null");
            }
            var entity = await QueryAll().FirstOrDefaultAsync(expression);
            if (expression == null)
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity was not found");
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(T)} could not be deleted: {ex.Message}");
            }
        }


        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null)
        {
            return await Task.Run(() => _context.Set<T>().WhereIf(expression).Count());
        }
    }
}
