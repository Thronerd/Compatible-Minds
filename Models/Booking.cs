using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Booking
    {
        [Key]
        public int bookingId { get; set; }

        public string studentNo { get; set; }
        public virtual Student student { get; set; }
        public string residenceCode { get; set; }
        public string RoomType { get; set; }
        public string roomId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Bookingdate { get; set; }
        public string year { get; set; }
        public string blockCode { get; set; }
        public string ResCode { get; set; }
        public virtual Residence Residence { get; set; }
        public virtual ICollection<Registraction> Registraction { get; set; }
        public virtual ICollection<Allocation> Allocation { get; set; }




    }
}