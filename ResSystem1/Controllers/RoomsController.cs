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
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Residence);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roomId,beds,roomType,status,residenceCode")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(rooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName", rooms.residenceCode);
            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName", rooms.residenceCode);
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roomId,beds,roomType,status,residenceCode")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rooms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName", rooms.residenceCode);
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Rooms rooms = db.Rooms.Find(id);
            db.Rooms.Remove(rooms);
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
