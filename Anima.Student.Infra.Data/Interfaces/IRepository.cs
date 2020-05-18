using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Anima.Student.Infra.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity _obj);
        Task UpdateAsync(TEntity _obj);
        Task RemoveAsync(TEntity _obj);
        void Dispose();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(long id);
        void Detach(TEntity _obj);
        void Attach(TEntity _obj);
        Task<Tuple<IEnumerable<TEntity>, int>> GetAllAsync
        (
            int skip,
            int take,
            Expression<Func<TEntity, bool>> where,
            bool asNoTracking = true
        );
    }
}