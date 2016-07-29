using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Logic;
using Models;

namespace ResSystem1.Controllers
{
    public class ApplicationsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Business b = new Business();
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                var comm = db.ApplyingCommands.ToList();
                var student = db.Students.Find(User.Identity.Name);
                var found = b.getAllResidences();
                var FoundInOrder = found.OrderBy(x => x.ResName);
                foreach(ApplyingCommand ac in comm)
                {
                    if(ac.ApplyingC_Date != DateTime.Today)
                    {
                        return View(FoundInOrder);
                    }
                        
                }
                return HttpNotFound();
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        public ActionResult Details(string id)
        {
            //ViewBag.RmType = new SelectList(b.RoomsByRes(id), "roomType", "roomType");

            var res = b.ResDetails(id);
            List<ApplyingCommand> list = db.ApplyingCommands.ToList();
            var _choices = db.Bookings.Where(x => x.studentNo == User.Identity.Name).ToList();
            Session["ResCode"] = id;
            foreach (ApplyingCommand a in list)
            {
                ViewBag.Semester = a.Semester;
                ViewBag.Year = a.ApplyingYear;
                var _Year = _choices.Where(x => x.year == Convert.ToString(a.ApplyingYear)).ToList();
                string Block = "";
                if (a.Semester == "Semester One")
                {
                    Block = "21";
                }
                else
                  if (a.Semester == "Semester Two")
                {
                    Block = "22";
                }
                else
                {
                    Block = "Year";
                }

                var _Semester = _Year.Where(x => x.blockCode == Block).ToList();
                Session["Times"] = "Preferences: " + (3 - _Semester.Count()) + " left";
                Session["DisableAction"] = (3 - _Semester.Count());
            }
            
            if(res.ResName == "Baltimore")
            {
                ViewBag.RmTple = "Sharing";
                ViewBag.Details = "Room Type: 3 Sleepers and 4 Sleepers. "  + "For 3 Spleepers: 3 Beds, 3 Wardrobe and 1 Fridge, "  + "and For 4 Spleepers: 4 Beds, 4 Wardrobe and 1 Fridge";
            }
            else
                if(res.ResName == "Alpine")
            {
                ViewBag.RmTple = "Single";
                ViewBag.Details = "1 Beds, 1 Wardrobe and 1 Fridge ";
            }
            else
                if (res.ResName == "Triton House")
            {
                ViewBag.RmTple = "Sharing";
                ViewBag.Details = "2 Beds with covers, 2 Wardrobe and 1 Fridge";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult BookingMade()
        {
            if(Request.IsAuthenticated)
            {
                var Std = User.Identity.Name;
               Session["booking"]= db.Bookings.Where(x => x.studentNo == Std).ToList();

                if (Session["booking"] == null)
                {
                    ViewBag.error = "No admitted residence Applications Found or you cannot register at this stage or the room is not available yet.";
                }

                return View(Session["booking"]);

            }
            

            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult ApMade()
        {
            var Std = User.Identity.Name;
            var found = db.Bookings.Where(x => x.studentNo == Std).ToList();
            var order = found.OrderBy(x => x.year);
            var orderSemester = order.OrderBy(x => x.blockCode);
            return View(orderSemester);
        }
     
    }
}