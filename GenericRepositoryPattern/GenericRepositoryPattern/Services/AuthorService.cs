using GenericRepositoryPattern.Models;
using GenericRepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GenericRepositoryPattern.Services
{
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        public AuthorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IQueryable<Author> GetAllAuthor()
        {
            return base.GetAll();
        }

        public IQueryable<Author> GetAllAuthor(params Expression<Func<Author, object>>[] includes)
        {
            return base.GetAllWithIncludes(includes);
        }

        public Author GetAuthor(int id)
        {
            return base.GetSingle(id);
        }

        public Author GetAuthor(Expression<Func<Author, bool>> predicated, params Expression<Func<Author, object>>[] includes)
        {
            return base.GetSingleWithIncludes(predicated, includes);
        }

        public IQueryable<Author> FindAuthorBy(Expression<Func<Author, bool>> predicate)
        {
            return base.FindBy(predicate);
        }

        public void CreateAuthor(Author entity)
        {
            base.Create(entity);
        }

        public void DeleteAuthor(Author entity)
        {
            base.Delete(entity);
        }

        public void UpdateAuthor(Author entity)
        {
            base.Update(entity);
        }

        public void SaveChanges()
        {
            base.Save();
        }
    }
}