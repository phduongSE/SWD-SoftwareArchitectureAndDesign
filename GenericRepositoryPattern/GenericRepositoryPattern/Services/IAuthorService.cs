using GenericRepositoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Services
{
    public interface IAuthorService
    {
        IQueryable<Author> GetAllAuthor();
        IQueryable<Author> GetAllAuthor(params Expression<Func<Author, object>>[] includes);
        Author GetAuthor(int id);
        Author GetAuthor(Expression<Func<Author, bool>> predicated, params Expression<Func<Author, object>>[] includes);
        IQueryable<Author> FindAuthorBy(Expression<Func<Author, bool>> predicate);
        void CreateAuthor(Author entity);
        void DeleteAuthor(Author entity);
        void UpdateAuthor(Author entity);
        void SaveChanges();
    }
}
