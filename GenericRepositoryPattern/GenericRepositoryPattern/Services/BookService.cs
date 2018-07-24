
using GenericRepositoryPattern.Models;
using GenericRepositoryPattern.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenericRepositoryPattern.Services
{
    public class BookService : BaseService<Book>, IBookService
    {
        public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IQueryable<Book> GetAllBook()
        {
            return base.GetAll();
        }

        public IQueryable<Book> GetAllBook(params Expression<Func<Book, object>>[] includes)
        {
            return base.GetAllWithIncludes(includes);
        }

        public Book GetBook(int id)
        {
            return base.GetSingle(id);
        }

        public Book GetBook(Expression<Func<Book, bool>> predicated, params Expression<Func<Book, object>>[] includes)
        {
            return base.GetSingleWithIncludes(predicated, includes);
        }

        public IQueryable<Book> FindBookBy(Expression<Func<Book, bool>> predicate)
        {
            return base.FindBy(predicate);
        }

        public void CreateBook(Book entity)
        {
            base.Create(entity);
        }

        public void DeleteBook(Book entity)
        {
            base.Delete(entity);
        }

        public void UpdateBook(Book entity)
        {
            base.Update(entity);
        }

        public void SaveChanges()
        {
            base.Save();
        }
    }
}