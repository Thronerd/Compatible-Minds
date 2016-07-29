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
using System.Net.Mail;
using System.Web.Security;

namespace ResSystem1.Controllers
{
    public class AdminsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult AssignRoles()
        {
            //if(User.IsInRole("Res Admin"))
            //{
                ViewBag.roles = new SelectList(Roles.GetAllRoles());
                ViewBag.Membership = new SelectList(db.Users.ToList(), "UserName", "UserName");
                //ViewBag.Membership = new SelectList(uc.UserProfiles.ToList(), "UserName", "UserName");
                return View();
            //}
            //else
            //{
            //    return HttpNotFound();
            //}
            
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AssignRoles(string membership, string roles)
        {
            if (!Roles.IsUserInRole(roles))
            {
                Roles.AddUserToRole(membership, roles);
                Session["RoleUserFeedback"] = membership + " Succesfully Assigned to " + roles + " role";
            }
            return RedirectToAction("AssignRoles");
        }
        public ActionResult AddNewRole()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddNewRole(string NewRole)
        {
            try
            {
                if (!Roles.RoleExists(NewRole))
                {
                    Roles.CreateRole(NewRole);
                    Session["RoleFeedback"] = "Role successfully added.";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Sorry Role was not added " + e.Message;
                return View();
            }
            return RedirectToAction("AssignRoles");
        }

        //public ActionResult SendEmail()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SendEmail(string To, string Subject, string Body)
        //{
        //    Email M = new Email();
        //    MailAddress From = new MailAddress("Thronerd@gmail.com");
        //    List<MailAddress> list = new List<MailAddress>();
        //    list.Add(new MailAddress(To));

        //    ViewBag.FeedBack = M.SendEmail(From, list, Body, Subject, false);
        //    return View();
        //}
        public ActionResult SendEmailToAllUsers()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SendEmailToAllUsers(string Subject, string Body)
        //{
        //    Email M = new Email();


        //    MailAddress From = new MailAddress("OnlineEnlightenBST@gmail.com");
        //    List<MailAddress> list = new List<MailAddress>();
        //    foreach (var UserEmail in r.getAllUsers())
        //    {
        //        list.Add(new MailAddress(UserEmail.Email));
        //    }


        //    ViewBag.FeedBack = M.SendEmail(From, list, Body, Subject, false);
        //    return View();
        //}

        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,name,IdNo,contactNo")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,name,IdNo,contactNo")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
