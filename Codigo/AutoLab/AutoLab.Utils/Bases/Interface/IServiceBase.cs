using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Utils.Bases.Interface
{
    public interface IServiceBase<TEntity>
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
        Task<int> RemoveAllAsync();
        Task RemoveAsync(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, String orderBy = null, String includes = null);
        Task<IEnumerable<TEntity>> GetByParamsAsync(
    Expression<Func<TEntity, bool>> filter = null,
    String orderBy = null,
    String includeProps = null,
    bool asNoTracking = true);
    }

}
