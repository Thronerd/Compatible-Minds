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
    public class OnlinePaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OnlinePayments
        public ActionResult Index()
        {
            return View(db.OnlinePayments.ToList());
        }

        // GET: OnlinePayments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlinePayment onlinePayment = db.OnlinePayments.Find(id);
            if (onlinePayment == null)
            {
                return HttpNotFound();
            }
            return View(onlinePayment);
        }

        // GET: OnlinePayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OnlinePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefNo,cardType,cardholderName,cardNumber,ccvno,expiryDate,amount")] OnlinePayment onlinePayment)
        {
            if (ModelState.IsValid)
            {
                db.OnlinePayments.Add(onlinePayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(onlinePayment);
        }

        // GET: OnlinePayments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlinePayment onlinePayment = db.OnlinePayments.Find(id);
            if (onlinePayment == null)
            {
                return HttpNotFound();
            }
            return View(onlinePayment);
        }

        // POST: OnlinePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefNo,cardType,cardholderName,cardNumber,ccvno,expiryDate,amount")] OnlinePayment onlinePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(onlinePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(onlinePayment);
        }

        // GET: OnlinePayments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlinePayment onlinePayment = db.OnlinePayments.Find(id);
            if (onlinePayment == null)
            {
                return HttpNotFound();
            }
            return View(onlinePayment);
        }

        // POST: OnlinePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OnlinePayment onlinePayment = db.OnlinePayments.Find(id);
            db.OnlinePayments.Remove(onlinePayment);
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
