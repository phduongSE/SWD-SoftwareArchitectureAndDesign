using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenericRepositoryPattern.Models;
using GenericRepositoryPattern.Services;
using GenericRepositoryPattern.ViewModels;

namespace GenericRepositoryPattern.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            this._bookService = bookService;
            this._authorService = authorService;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns>Index view with all books</returns>
        public ActionResult Index()
        {
            var books = _bookService.GetAllBook(x => x.Author);
            var booklist = books.ToList();
            var result = AutoMapper.Mapper.Map<List<Book>, List<BookVM>>(books.ToList());
            return View(result);
        }

        /// <summary>
        /// Details of a Book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int bookId = id ?? default(int);
            Book book = _bookService.GetBook(x => x.Id == bookId, x => x.Author);
            BookVM vm = AutoMapper.Mapper.Map<Book, BookVM>(book);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        /// <summary>
        /// Create View
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            List<Author> authors = _authorService.GetAllAuthor().ToList();
            ViewBag.AuthorId = new SelectList(authors, "Id", "Name", authors.First());
            return View();
        }

        /// <summary>
        /// Create a Book action
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,PublishDate,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(book);
                _bookService.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(null, "Id", "Name", book.AuthorId);
            return View(book);
        }

        /// <summary>
        /// Edit a book View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int bookId = id ?? default(int);
            Book book = _bookService.GetBook(bookId);
            BookEM bookEM = AutoMapper.Mapper.Map<Book, BookEM>(book);

            if (book == null)
            {
                return HttpNotFound();
            }

            List<Author> authors = _authorService.GetAllAuthor().ToList();
            ViewBag.AuthorId = new SelectList(authors, "Id", "Name", bookEM.AuthorId);

            return View(bookEM);
        }

        /// <summary>
        /// Edit a book action
        /// </summary>
        /// <param name="bookEM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,PublishDate,AuthorId")] BookEM bookEM)
        {
            if (ModelState.IsValid)
            {
                Book book = AutoMapper.Mapper.Map<BookEM, Book>(bookEM);

                _bookService.UpdateBook(book);
                _bookService.SaveChanges();

                return RedirectToAction("Index");
            }

            List<Author> authors = _authorService.GetAllAuthor().ToList();
            ViewBag.AuthorId = new SelectList(authors, "Id", "Name", bookEM.AuthorId);

            return View(bookEM);
        }

        /// <summary>
        /// Delete a Book View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int bookId = id ?? default(int);
            Book book = _bookService.GetBook(b => b.Id == bookId, b => b.Author);
            BookVM bookVM = AutoMapper.Mapper.Map<Book, BookVM>(book);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(bookVM);
        }

        /// <summary>
        /// Delete a Book action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _bookService.FindBookBy(b => b.Id == id).FirstOrDefault();

            _bookService.DeleteBook(book);
            _bookService.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
