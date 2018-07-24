using GenericRepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GenericRepositoryPattern.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = this._unitOfWork.GetRepository<T>();
        }

        protected virtual IQueryable<T> GetAll()
        {
            return this._repository.GetAll();
        }

        protected virtual IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            return this._repository.GetAllWithIncludes(includes);
        }

        protected virtual T GetSingle(int id)
        {
            return this._repository.GetSingle(id);
        }

        protected virtual T GetSingleWithIncludes(Expression<Func<T, bool>> predicated, params Expression<Func<T, object>>[] includes)
        {
            return this._repository.GetSingleWithIncludes(predicated, includes);
        }

        protected virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this._repository.FindBy(predicate);
        }

        protected virtual void Create(T entity)
        {
            this._repository.Create(entity);
        }

        protected virtual void Delete(T entity)
        {
            this._repository.Delete(entity);
        }

        protected virtual void Update(T entity)
        {
            this._repository.Update(entity);
        }

        protected virtual void Save()
        {
            this._unitOfWork.SaveChanges();
        }
    }
}