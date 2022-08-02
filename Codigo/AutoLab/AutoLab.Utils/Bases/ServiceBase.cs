using AutoLab.Utils.Bases.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Utils.Bases
{
    public class ServiceBase<TEntity> :IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public virtual void Add(TEntity entity)
        {
            repositoryBase.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await repositoryBase.AddAsync(entity);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, string orderBy = null, string includes = null)
        {
            return repositoryBase.Get(conditions, orderBy, includes);
        }
        public async Task<IEnumerable<TEntity>> GetByParamsAsync(
    Expression<Func<TEntity, bool>> filter = null,
    string orderBy = null,
    string includeProps = null,
    bool asNoTracking = true)
        {
            return await repositoryBase.GetByParamsAsync(filter, orderBy, includeProps, asNoTracking);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repositoryBase.GetAll();
        }
        public virtual TEntity GetById(Int32 id)
        {
            return repositoryBase.GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Int32 id)
        {
            return await repositoryBase.GetByIdAsync(id);
        }
        public virtual void Remove(TEntity entity)
        {
            repositoryBase.Remove(entity);
        }

        public virtual void Remove(Int32 id)
        {
            repositoryBase.Remove(id);
        }

        public async Task<int> RemoveAllAsync()
        {
            return await repositoryBase.RemoveAllAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            await repositoryBase.RemoveAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            repositoryBase.Update(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await repositoryBase.UpdateAsync(entity);
        }
    }

}
