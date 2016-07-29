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

namespace ResSystem1.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentNo,FirstName,LastName,gender,DOB,emailAddress,contactNo,blockCode,yearOfStudy,course,physicalAddress,registrationYr,NoOfModules,funder,levelOfStudy,financialStatus")] Student st)
        {
                StreamWriter wr = new StreamWriter(@"C:\\Users\\samsungpc\\Desktop\\Students.txt", true);

                wr.WriteLine(st.studentNo);
                wr.WriteLine(st.FirstName);
                wr.WriteLine(st.gender);
                wr.WriteLine(st.DOB);
                wr.WriteLine(st.emailAddress);
                wr.WriteLine(st.blockCode);
                wr.WriteLine(st.yearOfStudy);
                wr.WriteLine(st.course);
                wr.WriteLine(st.physicalAddress);
                wr.WriteLine(st.registrationYr);
                wr.WriteLine(st.NoOfModules);
                wr.WriteLine(st.funder);
                wr.WriteLine(st.levelOfStudy);
                wr.WriteLine(st.financialStatus);
            //db.Students.Add(student);
            //db.SaveChanges();
            ViewBag.Test = "Done";
            return View();
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentNo,FirstName,LastName,gender,DOB,emailAddress,contactNo,blockCode,yearOfStudy,course,physicalAddress,registrationYr,NoOfModules,funder,levelOfStudy,financialStatus")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
