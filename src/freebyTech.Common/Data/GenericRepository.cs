using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using freebyTech.Common.Data.Interfaces;
using freebyTech.Common.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace freebyTech.Common.Data
{
    public class GenericRepository<TEntity> where TEntity : class, IEditableModel, IFindableByGuid
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> All()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = query.Where(predicate).ToList();
            return results;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity FindById(Guid id)
        {
            return _dbSet.AsNoTracking().SingleOrDefault(i => i.Id == id);
        }
        
        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public TEntity FindByProperty(string propertyName, object propertyValue)
        {
            return _dbSet.AsNoTracking().SingleOrDefault(BuildLambdaForPropertySearch(propertyName, propertyValue));
        }

        public void Insert(TEntity entity, string userName)
        {
            entity.UpdateIfEdited(userName);
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity, string userName)
        {
            entity.UpdateIfEdited(userName);
            _dbSet.Attach(entity).State = EntityState.Modified;
        }

        #region Helper Methods

        private Expression<Func<TEntity, bool>> BuildLambdaForPropertySearch(string propertyName, object propertyValue)
        {
            var item = Expression.Parameter(typeof(TEntity), "entity");
            var prop = Expression.Property(item, propertyName);
            var value = Expression.Constant(propertyValue);
            var equal = Expression.Equal(prop, value);
            return Expression.Lambda<Func<TEntity, bool>>(equal, item);
        }

        private IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        #endregion
    }
}
