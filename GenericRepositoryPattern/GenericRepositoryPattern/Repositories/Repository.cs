using GenericRepositoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GenericRepositoryPattern.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = this._dbContext.Set<T>();
            return query;
        }

        public T GetSingle(int id)
        {
            return this._dbSet.Find(id);
        }

        public IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = this._dbSet;
            foreach (var expression in includes)
            {
                result = result.Include(expression);
            }
            return result;
        }

        public T GetSingleWithIncludes(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes)
        {
            return this.GetAllWithIncludes(includes).FirstOrDefault(predicated);
        }

        public void Create(T entity)
        {
            this._dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this._dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this._dbContext.Set<T>().Where(predicate);
            return query;
        }
    }
}