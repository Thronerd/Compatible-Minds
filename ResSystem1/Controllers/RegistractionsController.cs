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
    public class RegistractionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Registractions
        public ActionResult Index()
        {
            var registractions = db.Registractions.Include(r => r.Booking).Include(r => r.Student);
            return View(registractions.ToList());
        }

        // GET: Registractions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registraction registraction = db.Registractions.Find(id);
            if (registraction == null)
            {
                return HttpNotFound();
            }
            return View(registraction);
        }

        // GET: Registractions/Create
        public ActionResult Create()
        {
            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo");
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName");
            return View();
        }

        // POST: Registractions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegNum,studentNo,bookingId")] Registraction registraction)
        {
            if (ModelState.IsValid)
            {
                db.Registractions.Add(registraction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo", registraction.bookingId);
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName", registraction.studentNo);
            return View(registraction);
        }

        // GET: Registractions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registraction registraction = db.Registractions.Find(id);
            if (registraction == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo", registraction.bookingId);
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName", registraction.studentNo);
            return View(registraction);
        }

        // POST: Registractions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegNum,studentNo,bookingId")] Registraction registraction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registraction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo", registraction.bookingId);
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName", registraction.studentNo);
            return View(registraction);
        }

        // GET: Registractions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registraction registraction = db.Registractions.Find(id);
            if (registraction == null)
            {
                return HttpNotFound();
            }
            return View(registraction);
        }

        // POST: Registractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registraction registraction = db.Registractions.Find(id);
            db.Registractions.Remove(registraction);
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
