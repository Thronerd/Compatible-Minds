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
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create


             public ActionResult Create()
            {
              return   View();
            
            }

           [HttpPost]
         
        public ActionResult Create([Bind(Include = "recieptNo,amountPaid")] Payment payment, attachment attach)
        {
            if(!ModelState.IsValid)
            {
                return View(attach);
            }
           
               byte[] uploadedFile = new byte[attach.File.InputStream.Length];
                attach.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

           

                //var payment = new Payment();
                payment.FileName = attach.File.FileName;
                payment.File = uploadedFile;
                db.Payments.Add(payment);
                db.SaveChanges();
                ViewBag.Content = "payment succesfully made";
             
                return RedirectToAction("Index");
            }


        

        // GET: Payments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recieptNo,amountPaid,FileName,File")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
