using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bookish.Web.Models;

namespace Bookish.Web.Controllers
{
    public class BookInfoesController : Controller
    {
        private BookishEntities db = new BookishEntities();

        // GET: BookInfoes
        public ActionResult Index(string searchBy, string searchString)
        {
            var books = from b in db.BookInfoes select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                
                books = books.Where(s => s.BookName.Contains(searchString) || searchString == null);
                
            }
            else
            {
               
            }
            

            return View(books.ToList());
        }

        // GET: BookInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfo bookInfo = db.BookInfoes.Find(id);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // GET: BookInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BookName,BookGenre,BookYear")] BookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                db.BookInfoes.Add(bookInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookInfo);
        }

        // GET: BookInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfo bookInfo = db.BookInfoes.Find(id);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // POST: BookInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BookName,BookGenre,BookYear")] BookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookInfo);
        }

        // GET: BookInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfo bookInfo = db.BookInfoes.Find(id);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // POST: BookInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookInfo bookInfo = db.BookInfoes.Find(id);
            db.BookInfoes.Remove(bookInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
