using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Registraction
    {
        [Key]
        public int RegNum { get; set; }
        public string studentNo { get; set; }
        public int bookingId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
