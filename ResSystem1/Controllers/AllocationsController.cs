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
    public class AllocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Allocations
        public ActionResult Index()
        {
            var allocations = db.Allocations.Include(a => a.Booking);
            return View(allocations.ToList());
        }

        // GET: Allocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allocation allocation = db.Allocations.Find(id);
            if (allocation == null)
            {
                return HttpNotFound();
            }
            return View(allocation);
        }

        // GET: Allocations/Create
        public ActionResult Create()
        {
            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo");
            return View();
        }

        // POST: Allocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllocationRefNo,bookingId,residenceCode,roomId,RoomType,blockCode")] Allocation allocation)
        {
            if (ModelState.IsValid)
            {
                db.Allocations.Add(allocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo", allocation.bookingId);
            return View(allocation);
        }

        // GET: Allocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allocation allocation = db.Allocations.Find(id);
            if (allocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo", allocation.bookingId);
            return View(allocation);
        }

        // POST: Allocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllocationRefNo,bookingId,residenceCode,roomId,RoomType,blockCode")] Allocation allocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookingId = new SelectList(db.Bookings, "bookingId", "studentNo", allocation.bookingId);
            return View(allocation);
        }

        // GET: Allocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allocation allocation = db.Allocations.Find(id);
            if (allocation == null)
            {
                return HttpNotFound();
            }
            return View(allocation);
        }

        // POST: Allocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Allocation allocation = db.Allocations.Find(id);
            db.Allocations.Remove(allocation);
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
