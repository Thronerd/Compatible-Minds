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
    public class FundersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Funders
        public ActionResult Index()
        {
            return View(db.Funders.ToList());
        }

        // GET: Funders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funder funder = db.Funders.Find(id);
            if (funder == null)
            {
                return HttpNotFound();
            }
            return View(funder);
        }

        // GET: Funders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "funderId,funderName")] Funder funder)
        {
            if (ModelState.IsValid)
            {
                db.Funders.Add(funder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funder);
        }

        // GET: Funders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funder funder = db.Funders.Find(id);
            if (funder == null)
            {
                return HttpNotFound();
            }
            return View(funder);
        }

        // POST: Funders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "funderId,funderName")] Funder funder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funder);
        }

        // GET: Funders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funder funder = db.Funders.Find(id);
            if (funder == null)
            {
                return HttpNotFound();
            }
            return View(funder);
        }

        // POST: Funders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funder funder = db.Funders.Find(id);
            db.Funders.Remove(funder);
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
