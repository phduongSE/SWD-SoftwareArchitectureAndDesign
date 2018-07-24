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
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        // GET: Authors
        public ActionResult Index()
        {
            List<Author> authors = _authorService.GetAllAuthor().ToList();
            List<AuthorVM> authorVMs = AutoMapper.Mapper.Map<List<Author>, List<AuthorVM>>(authors);

            return View(authorVMs);
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Author author = _authorService.GetAuthor(a => a.Id == id, a => a.BookList);
            AuthorDetail authorDetail = AutoMapper.Mapper.Map<Author, AuthorDetail>(author);

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(authorDetail);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BirthDay")] AuthorVM authorVM)
        {
            if (ModelState.IsValid)
            {
                Author author = AutoMapper.Mapper.Map<AuthorVM, Author>(authorVM);

                _authorService.CreateAuthor(author);
                _authorService.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(authorVM);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Author author = _authorService.FindAuthorBy(a => a.Id == id).FirstOrDefault();
            
            if (author == null)
            {
                return HttpNotFound();
            }

            AuthorVM authorVM = AutoMapper.Mapper.Map<Author, AuthorVM>(author);
            return View(authorVM);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDay")] AuthorVM authorVM)
        {
            if (ModelState.IsValid)
            {
                Author author = AutoMapper.Mapper.Map<AuthorVM, Author>(authorVM);
                _authorService.UpdateAuthor(author);
                _authorService.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(authorVM);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = _authorService.FindAuthorBy(a => a.Id == id).FirstOrDefault();

            if (author == null)
            {
                return HttpNotFound();
            }

            AuthorVM authorVM = AutoMapper.Mapper.Map<Author, AuthorVM>(author);
            return View(authorVM);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = _authorService.FindAuthorBy(a => a.Id == id).FirstOrDefault();
            _authorService.DeleteAuthor(author);
            _authorService.SaveChanges();
            
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
