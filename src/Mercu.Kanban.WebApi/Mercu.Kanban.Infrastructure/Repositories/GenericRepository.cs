using Mercu.Kanban.Domain.Interfaces;
using Mercu.Kanban.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mercu.Kanban.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DatabaseContext _databaseContext;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<TEntity>();
        }

        public virtual async Task<IList<TEntity>> GetWithConditions(
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entity = await query.FirstOrDefaultAsync();
#pragma warning disable CS8603 // Possible null reference return.
            return entity;
#pragma warning restore CS8603 // Possible null reference return.

        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IList<TEntity> entitys)
        {
            await _dbSet.AddRangeAsync(entitys);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IList<TEntity> entitys)
        {
            _dbSet.RemoveRange(entitys);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IList<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual Task<TEntity>? GetWithIncludeAsync(Expression<Func<TEntity, bool>> expression) => null;
    }
}
