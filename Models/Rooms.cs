using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Models
{
    public class Rooms
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string roomId { get; set; }


        public int beds { get; set; }
        public string roomType { get; set; }
        public int Quantity { get; set; }
        public string status { get; set; }
        public string residenceCode { get; set; }
        public string ResCode { get; set; }
        public int AllocatedTimes { get; set; }
        public virtual Residence Residence { get; set; }
        public virtual ICollection<Allocation> Allocation { get; set; }


    }
}