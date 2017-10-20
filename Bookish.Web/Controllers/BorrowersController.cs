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
    public class BorrowersController : Controller
    {
        private BookishEntities db = new BookishEntities();

        // GET: Borrowers
        public ActionResult Index()
        {
            var borrowers = db.Borrowers.Include(b => b.Borrow);
            return View(borrowers.ToList());
        }

        // GET: Borrowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // GET: Borrowers/Create
        public ActionResult Create()
        {
            ViewBag.BorrowerID = new SelectList(db.Borrows, "BorrowID", "BorrowID");
            return View();
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowerID,FirstName,MiddleName,LastName,DateOfBirth")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Borrowers.Add(borrower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BorrowerID = new SelectList(db.Borrows, "BorrowID", "BorrowID", borrower.BorrowerID);
            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            ViewBag.BorrowerID = new SelectList(db.Borrows, "BorrowID", "BorrowID", borrower.BorrowerID);
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowerID,FirstName,MiddleName,LastName,DateOfBirth")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BorrowerID = new SelectList(db.Borrows, "BorrowID", "BorrowID", borrower.BorrowerID);
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            db.Borrowers.Remove(borrower);
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
