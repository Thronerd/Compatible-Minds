using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Visitor
    {
        [Key]
        public string visitorId { get; set; }
        public string Fname { get; set; }
        public DateTime timeIn { get; set; }
        public DateTime timeOut { get; set; }
        public string studentNo { get; set; }
        public virtual Student student { get; set; }
    }
}