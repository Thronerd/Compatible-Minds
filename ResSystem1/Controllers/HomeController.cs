using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResSystem1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("StudentConf");
            }
            
        }
        public ActionResult HomeIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult ValidStudent(string Username)
        {
            string Feedback = "Invalid Student Number";
            string StudNo = "";
            string Fname = "";
            string gender = "";
            string DOB = "";
            string emailAddress = "";
            int contactNo = 0;
            string blockCode = "";
            string yearOfStudy = "";
            string course = "";
            string physicalAddress = "";
            string registrationYr = "";
            int NoOfModules = 0;
            string funder = "";
            string levelOfStudy = "";
            string financialStatus = ""; 
            StreamReader reader = new StreamReader(@"C:\\Users\\samsungpc\\Desktop\\Students.txt");
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                while (reader.Peek() != -1)
                {
                    StudNo = reader.ReadLine();
                    Fname = reader.ReadLine();
                    gender = reader.ReadLine();
                    DOB = reader.ReadLine();
                    emailAddress = reader.ReadLine();
                    contactNo = Convert.ToInt16(reader.ReadLine());
                    blockCode = reader.ReadLine();
                    yearOfStudy = reader.ReadLine();
                    course = reader.ReadLine();
                    physicalAddress = reader.ReadLine();
                    registrationYr = reader.ReadLine();
                    NoOfModules = Convert.ToInt16(reader.ReadLine());
                    funder = reader.ReadLine();
                    levelOfStudy = reader.ReadLine();
                    financialStatus = reader.ReadLine();


                    if (StudNo == Username && db.Users.Find(Username) == null)
                    {
                        Feedback = "Valid student number, please procced";
                    }
                }
                return Json(Feedback, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(Feedback, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult StudentConf()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentConf(string Username)
        {
            string StudNo = "";
            string Fname = "";
            string Lname = "";
            string gender = "";
            string DOB = "";
            string emailAddress = "";
            int contactNo = 0;
            string blockCode = "";
            string yearOfStudy = "";
            string course = "";
            string physicalAddress = "";
            string registrationYr = "";
            string NoOfModules = "";
            string funder = "";
            string levelOfStudy = "";
            string financialStatus = "";

            ApplicationDbContext db = new ApplicationDbContext();
           
                StreamReader reader = new StreamReader(@"C:\\Users\\samsungpc\\Desktop\\Students.txt", true);
                while (reader.Peek() != -1)
                {
                    StudNo = reader.ReadLine();
                    Fname = reader.ReadLine();
                Lname = reader.ReadLine();
                gender = reader.ReadLine();
                    DOB = reader.ReadLine();
                    emailAddress = reader.ReadLine();
                    contactNo = Convert.ToInt32(reader.ReadLine());
                    blockCode = reader.ReadLine();
                    yearOfStudy = reader.ReadLine();
                    course = reader.ReadLine();
                    physicalAddress = reader.ReadLine();
                    registrationYr = reader.ReadLine();
                    NoOfModules = reader.ReadLine();
                    funder = reader.ReadLine();
                    levelOfStudy = reader.ReadLine();
                    financialStatus = reader.ReadLine();


                if (StudNo == Username && db.Students.Where(x=> x.studentNo == Username).ToList().Count() == 0)
                    {
                    string[] password = DOB.Split('/');
                    Session["Username"] = Username;
                    Session["EmailAddress"] = Username + "@dut4life.ac.za";
                    Session["Password"] = "Dut" + password[2].Substring(2, 2) + password[1] + password[0] + "!";
                        return RedirectToAction("Register", "Account");
                    }
                else
                 if (StudNo == Username && db.Students.Where(x => x.studentNo == Username).ToList().Count() != 0)
                {
                    Session["Username"] = Username;
                    return RedirectToAction("Login", "Account");
                }
                        
                }
            ViewBag.FeedBack = "Invalid student number. Pleace contact: ";
                return View();
        }
    }
}