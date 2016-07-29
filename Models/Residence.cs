using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Residence
    {

        [Key]
        public string residenceCode { get; set; }
        public string ResName { get; set; }
        public string Gender { get; set; }
        public string address { get; set; }
        public string campus { get; set; }
        public string contactNo { get; set; }
        public int noOfRooms { get; set; }
        public string RoomType { get; set; }
        public int capacity { get; set; }
        public string Levels { get; set; }
        public string Courses { get; set; }
        public double Cost { get; set;}
        public virtual ICollection<Rooms> Rooms { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }

    }
}