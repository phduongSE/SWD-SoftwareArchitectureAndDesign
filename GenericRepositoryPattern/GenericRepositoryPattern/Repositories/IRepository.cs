namespace GenericRepositoryPattern.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes);
        T GetSingle(int id);
        T GetSingleWithIncludes(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
