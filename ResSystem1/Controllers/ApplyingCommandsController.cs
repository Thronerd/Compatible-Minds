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
using Logic;

namespace ResSystem1.Controllers
{
    public class ApplyingCommandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Business b = new Business();

        // GET: ApplyingCommands
        public ActionResult Index()
        {
            return View(db.ApplyingCommands.ToList());
        }

        // GET: ApplyingCommands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyingCommand applyingCommand = db.ApplyingCommands.Find(id);
            if (applyingCommand == null)
            {
                return HttpNotFound();
            }
            return View(applyingCommand);
        }

        // GET: ApplyingCommands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplyingCommands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplyingCommand commands, DateTime ClDate, DateTime OpDate, string Semester, int Year)
        {
            //try
            //{
            var list = db.ApplyingCommands.ToList();

            if (b.FindCommand(Year) != null)
            {
                UpdateCommmand(ClDate, OpDate,Semester, Year);
            }
            else
            {
                
                foreach(ApplyingCommand a in list)
                {
                    db.ApplyingCommands.Remove(a);
                    db.SaveChanges();
                }
                commands.ApplyingC_Date = ClDate;
                commands.ApplyingO_Date = OpDate;
                commands.ApplyingYear = Year;
                commands.Semester = Semester;
                db.ApplyingCommands.Add(commands);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
            //}
            //catch
            //{

            //    return View(commands);
            //}

        }

        // GET: ApplyingCommands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyingCommand applyingCommand = db.ApplyingCommands.Find(id);
            if (applyingCommand == null)
            {
                return HttpNotFound();
            }
            return View(applyingCommand);
        }

        // POST: ApplyingCommands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplyingYear,ApplyingO_Date,ApplyingC_Date,Semester")] ApplyingCommand applyingCommand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applyingCommand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applyingCommand);
        }

        // GET: ApplyingCommands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyingCommand applyingCommand = db.ApplyingCommands.Find(id);
            if (applyingCommand == null)
            {
                return HttpNotFound();
            }
            return View(applyingCommand);
        }

        // POST: ApplyingCommands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplyingCommand applyingCommand = db.ApplyingCommands.Find(id);
            db.ApplyingCommands.Remove(applyingCommand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void UpdateCommmand(DateTime ClDate, DateTime OpDate, string Semester, int Year)
        {
            var found =db.ApplyingCommands.Find(Year);

            found.Semester = Semester;
            found.ApplyingO_Date = OpDate;
            found.ApplyingC_Date = ClDate;
            db.SaveChanges();
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
