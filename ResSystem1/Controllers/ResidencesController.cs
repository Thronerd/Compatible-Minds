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
    public class ResidencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Business b = new Business();
        // GET: Residences
        public ActionResult Index()
        {
            
            if (Request.IsAuthenticated)
            {
                var comm = db.ApplyingCommands.ToList();
                var std = b.OneStudent(User.Identity.Name);
                var ResList = db.Residences.ToList();
                foreach (ApplyingCommand ac in comm)
                {
                    if (ac.ApplyingC_Date != DateTime.Today)
                    {
                        switch (std.gender)
                        {
                            case "Male":
                                ResList = db.Residences.Where(x => x.Gender == "Male").ToList();
                                break;
                            case "Female":
                                ResList = db.Residences.Where(x => x.Gender == "Female").ToList();
                                break;
                            default:
                                ResList = db.Residences.Where(x => x.Gender == "Mixed").ToList();
                                break;
                        }

                        if (std.levelOfStudy == "Level 3" || std.levelOfStudy == "Level 4")
                        {
                            ResList = ResList.Where(x => x.Levels.Contains("3") || x.Levels.Contains("2")).ToList();
                        }
                        else
                        {
                            ResList = ResList.Where(x => x.Levels.Contains("4") || x.Levels.Contains("PHD")).ToList();
                        }


                        return View(db.Residences.ToList());
                    }

                }
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("StudentConf","Home");
            }

        }
        [HttpGet]
       public ActionResult Search(string Search)
        {
            List<Residence> found = new List<Residence>();
            if(string.IsNullOrEmpty(Search))
            {
                found = db.Residences.ToList();
            }
            else
            {
                found = db.Residences.Where(x => x.ResName.StartsWith(Search)).ToList();
            }
           
            return View(found);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.Residences.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // GET: Residences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Residences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "residenceCode,ResName,address,campus,contactNo,noOfRooms,capacity")] Residence residence)
        {
            if (ModelState.IsValid)
            {
                db.Residences.Add(residence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(residence);
        }

        // GET: Residences/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.Residences.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // POST: Residences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "residenceCode,ResName,address,campus,contactNo,noOfRooms,capacity")] Residence residence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(residence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(residence);
        }

        // GET: Residences/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.Residences.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // POST: Residences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Residence residence = db.Residences.Find(id);
            db.Residences.Remove(residence);
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
