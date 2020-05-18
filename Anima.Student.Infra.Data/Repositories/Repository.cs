using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anima.Student.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anima.Student.Infra.Data.Repositories
{
    
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Property

        protected Context.MySql.MySqlContext Db = new Context.MySql.MySqlContext();
        protected DbSet<TEntity> DbSet = new Context.MySql.MySqlContext().Set<TEntity>();
        
        #endregion
        
        #region # Methods

        public async Task AddAsync(TEntity _obj)
        {
            Db.Set<TEntity>().Add(_obj);
            Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Db.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await Db.Set<TEntity>().FindAsync(id);
        }

        public async Task RemoveAsync(TEntity _obj)
        {
            Db.Set<TEntity>().Remove(_obj);
            Db.SaveChanges();
        }

        public async Task UpdateAsync(TEntity _obj)
        {
            Db.Entry(_obj).State = EntityState.Modified;
            Db.SaveChangesAsync();
        }

        public void Detach(TEntity _obj)
        {
            Db.Entry(_obj).State = EntityState.Detached;
        }

        public void Attach(TEntity _obj)
        {
            DbSet.Attach(_obj);
        }
        
        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAllAsync
        (
            int skip,
            int take,
            Expression<Func<TEntity, bool>> where,
            bool asNoTracking = true
        )
        {
            var databaseCount = await DbSet.CountAsync(where);
            if (asNoTracking)
                return new Tuple<IEnumerable<TEntity>, int>
                (
                    await DbSet.AsNoTracking().Where(where).Skip(skip).Take(take).ToListAsync(),
                    databaseCount
                );

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await DbSet.Where(where).Skip(skip).Take(take).ToListAsync(),
                databaseCount
            );
        }
        
        #endregion

        #region # IDisponsable

        private Component component = new Component();
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    component.Dispose();

                disposed = true;
            }
        }
        #endregion
    }
}