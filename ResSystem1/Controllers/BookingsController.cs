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
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Residence).Include(b => b.student);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "bookingId,studentNo,residenceCode,RoomType,roomId,Bookingdate,year,blockCode,ResCode")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var found = db.Rooms.Find(booking.roomId);
                var std = db.Students.Find(booking.studentNo);
                var stdBk = db.Bookings.ToList().Find(x => x.studentNo == booking.studentNo);
                var count = db.Bookings.Where(x => x.roomId == booking.roomId).ToList();
                if (stdBk == null)
                {
                    if (count.Count() != found.Quantity)
                    {

                        if (found.Quantity > found.AllocatedTimes)
                        {
                            found.AllocatedTimes += 1;

                            if (found.Quantity == found.AllocatedTimes)
                            {
                                found.status = "Full";
                            }
                        }
                        db.Bookings.Add(booking);
                        db.SaveChanges();
                        //SMS
                        //var soapSms = new ResSystem1.ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
                        //soapSms.SendSimpleTextSMS(
                        //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSUSERKEY"],
                        //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSPASSWORD"], "+27" + std.contactNo,
                        //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSORIGINATOR"],
                        //    "Reservation made. For your reservation to be proceed Make sure all oustanding fees are paid befor registration date. For more info: 0315469870");
                        //soapSms.Close();
                        Session["ErorrBookings"] = "";
                    }
                    
                    
                }
                else
                {
                    Session["ErorrBookings"] = "Already Reserved a Room: " + stdBk.roomId + " at " + stdBk.Residence.ResName;
                }
                return RedirectToAction("Index");
            }

            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName", booking.residenceCode);
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName", booking.studentNo);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName", booking.residenceCode);
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName", booking.studentNo);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingId,studentNo,residenceCode,RoomType,roomId,Bookingdate,year,blockCode,ResCode")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.residenceCode = new SelectList(db.Residences, "residenceCode", "ResName", booking.residenceCode);
            ViewBag.studentNo = new SelectList(db.Students, "studentNo", "FirstName", booking.studentNo);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            var found = db.Rooms.Find(booking.roomId);
            var count = db.Bookings.Where(x => x.roomId == booking.roomId).ToList();
            if(found.AllocatedTimes > 0)
            {
                  found.AllocatedTimes -= 1;
                
                  found.status = "Not Full";
            }

            db.Bookings.Remove(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        public ActionResult getRooms(string Id)
        {
            int total = 0;
            List<ApplyingCommand> list = db.ApplyingCommands.ToList();
            var Res_Rooms = db.Rooms.Where(x => x.residenceCode == Id && x.status == "Not Full").ToList();
            foreach (ApplyingCommand a in list)
            {
                Session["Year"] = a.ApplyingYear;
                Session["Block"] = a.Semester;
               
            }

            var Res_RoomsAv2 = Res_Rooms.Where(x => x.roomType == "Single").ToList();
            var Res_RoomsAv3 = Res_Rooms.Where(x => x.roomType == "Sharing").ToList();
            Session["SpcfRoomAv"] = Res_RoomsAv3;
            foreach (Rooms r in Res_RoomsAv3)
            {
                //Session["SpcfRoomAv"] = "Only: " + (r.Quantity - r.AllocatedTimes) + " bed Space available";
                total += r.Quantity - r.AllocatedTimes;
            }
            total += Res_RoomsAv2.Count();
            ViewBag.NoOfRumsAv = total;
            Session["AvailTotal"] = total;
            return View(Res_Rooms);
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
