using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Student
    {
        [Key]
        public string studentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string gender { get; set; }
        public string DOB { get; set; }
        public string emailAddress { get; set; }
        public int contactNo { get; set; }
        public string blockCode { get; set; }
        public string yearOfStudy { get; set; }
        public string course { get; set; }
        public string physicalAddress { get; set; }
        public string registrationYr { get; set; }
        public int NoOfModules { get; set; }
        public string funder { get; set; }
        public string levelOfStudy { get; set; }
        public string financialStatus { get; set; }
        public ICollection<Visitor> visitor { get; set; }
        public ICollection<Registraction> Registraction { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}