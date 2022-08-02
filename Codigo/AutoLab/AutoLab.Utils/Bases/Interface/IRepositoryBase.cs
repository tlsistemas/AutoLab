using System.Data.Common;
using System.Linq.Expressions;

namespace AutoLab.Utils.Bases.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(Int32 id);
        Task<TEntity> GetByIdAsync(Int32 id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Remove(int id);
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<int> RemoveAllAsync();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, String orderBy = null, String includes = null);
        Task<IEnumerable<TEntity>> GetByParamsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            String orderBy = null,
            String includeProps = null,
            bool asNoTracking = true);
    }

}
