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
using System.IO;
using Logic;

namespace ResSystem1.Controllers
{
    public class FinancialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Business b = new Business();

        // GET: Financials
        public ActionResult FinancialStatemant()
        {
            string stdNum = "";
            DateTime date = DateTime.Now.Date;
            string recCod = "";
            string ResName = "";
            double ResCost = 0;
            string payment = "";
            double CostPayment = 0;
            var Res = db.Bookings.ToList().Find(x=> x.studentNo == User.Identity.Name);
            StreamReader reader = new StreamReader(@"C:\\Users\\samsungpc\\Desktop\\Students.txt", true);
            while(reader.Peek() != -1)
            {
                stdNum = reader.ReadLine();
                date = Convert.ToDateTime(reader.ReadLine());
                recCod = reader.ReadLine();
                ResName = reader.ReadLine();
                ResCost = Convert.ToDouble(reader.ReadLine());
                payment = reader.ReadLine();
                CostPayment = Convert.ToDouble(reader.ReadLine());

                if(stdNum == User.Identity.Name)
                {
                    ViewBag.date = date;
                    ViewBag.recCod = recCod;
                    ViewBag.ResName = ResName;
                    ViewBag.stdNum = stdNum;
                    ViewBag.ResCost = ResCost;
                    ViewBag.payment = payment;
                    ViewBag.CostPayment = CostPayment;
                    ViewBag.stdNum = stdNum;
                    //New
                    ViewBag.date2 = DateTime.Now.Date;
                    ViewBag.recCod2 = Res.residenceCode;
                    ViewBag.ResName2 = Res.Residence.ResName;
                    ViewBag.ResCost2 = Res.Residence.Cost;
                }
            }
            return View();
        }
        public ActionResult Index()
        {
            var financials = db.Financials.Include(f => f.onlinepayment);
            return View(financials.ToList());
        }

        // GET: Financials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financial financial = db.Financials.Find(id);
            if (financial == null)
            {
                return HttpNotFound();
            }
            return View(financial);
        }

        // GET: Financials/Create
        public ActionResult Create()
        {
            ViewBag.refNo = new SelectList(db.OnlinePayments, "RefNo", "cardType");
            return View();
        }

        // POST: Financials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "funderId,funder,refNo")] Financial financial)
        {
           
            StreamReader sr = new StreamReader(@"C:\\Users\\samsungpc\\Desktop\\Students.txt", true);

            string studNo = "";
            string funder = "";
            string financialStatus = "";

            try
            {
                while (sr.Peek() != -1)
                {
                    studNo = sr.ReadLine();
                    financialStatus = sr.ReadLine();
                    funder = sr.ReadLine();
                }

                if (User.Identity.Name == studNo && funder == "NSFAS" && financialStatus == "Up To Date")
                {
                    ViewBag.results = "Cleared, you can proceed with registration";
                }
                else if (User.Identity.Name == studNo && funder == "NSFAS" && financialStatus != "Up To Date")
                {
                    ViewBag.results = "Go visit financial aid for unblock code";
                }
                Session["status"] = ViewBag.results;
            }

            catch (DataException)

            {
                ModelState.AddModelError("", "Error occured");
               
            }
          


            return View(Session["status"]);
        }

        // GET: Financials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financial financial = db.Financials.Find(id);
            if (financial == null)
            {
                return HttpNotFound();
            }
            ViewBag.refNo = new SelectList(db.OnlinePayments, "RefNo", "cardType", financial.refNo);
            return View(financial);
        }

        // POST: Financials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "funderId,funder,refNo")] Financial financial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.refNo = new SelectList(db.OnlinePayments, "RefNo", "cardType", financial.refNo);
            return View(financial);
        }

        // GET: Financials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Financial financial = db.Financials.Find(id);
            if (financial == null)
            {
                return HttpNotFound();
            }
            return View(financial);
        }

        // POST: Financials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Financial financial = db.Financials.Find(id);
            db.Financials.Remove(financial);
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
