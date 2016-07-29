using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Allocation
    {
        [Key]
        public int AllocationRefNo { get; set; }
        public int bookingId { get; set; }
        public string residenceCode { get; set; }
        public string roomId { get; set; }
        public string RoomType { get; set; }
        public string blockCode { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
