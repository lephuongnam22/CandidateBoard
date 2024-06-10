using System.Linq.Expressions;

namespace Kanban.Board.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> expression);
        Task<IList<TEntity>> GetWithConditions(Expression<Func<TEntity, bool>> expression);
        Task<IList<TEntity>> GetAll();
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IList<TEntity> entitys);
        void Delete(TEntity entity);
        void DeleteRange(IList<TEntity> entitys);
        void Update(TEntity entity);
        void UpdateRange(IList<TEntity> entities);
        Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> expression);
    }
}
