using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Models;

namespace ResSystem1.Controllers
{
    public class OtherUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OtherUsers
        public ActionResult Index()
        {
            return View(db.OtherUsers.ToList());
        }

        // GET: OtherUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherUsers otherUsers = db.OtherUsers.Find(id);
            if (otherUsers == null)
            {
                return HttpNotFound();
            }
            return View(otherUsers);
        }

        // GET: OtherUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OtherUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,Fname,Lname,gender,emailAddress,contactNo")] OtherUsers otherUsers)
        {
            if (ModelState.IsValid)
            {
                db.OtherUsers.Add(otherUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(otherUsers);
        }

        // GET: OtherUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherUsers otherUsers = db.OtherUsers.Find(id);
            if (otherUsers == null)
            {
                return HttpNotFound();
            }
            return View(otherUsers);
        }

        // POST: OtherUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,Fname,Lname,gender,emailAddress,contactNo")] OtherUsers otherUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otherUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(otherUsers);
        }

        // GET: OtherUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherUsers otherUsers = db.OtherUsers.Find(id);
            if (otherUsers == null)
            {
                return HttpNotFound();
            }
            return View(otherUsers);
        }

        // POST: OtherUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OtherUsers otherUsers = db.OtherUsers.Find(id);
            db.OtherUsers.Remove(otherUsers);
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
