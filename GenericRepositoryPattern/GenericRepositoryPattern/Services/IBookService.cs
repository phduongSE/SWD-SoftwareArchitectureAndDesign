using GenericRepositoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Services
{
    public interface IBookService
    {
        IQueryable<Book> GetAllBook();
        IQueryable<Book> GetAllBook(params Expression<Func<Book, object>>[] includes);
        Book GetBook(int id);
        Book GetBook(Expression<Func<Book, bool>> predicated, params Expression<Func<Book, object>>[] includes);
        IQueryable<Book> FindBookBy(Expression<Func<Book, bool>> predicate);
        void CreateBook(Book entity);
        void DeleteBook(Book entity);
        void UpdateBook(Book entity);
        void SaveChanges();
    }
}
