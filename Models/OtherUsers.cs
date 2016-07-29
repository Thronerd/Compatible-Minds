using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class OtherUsers
    {
        [Key]
        public string userId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string gender { get; set; }
        public string emailAddress { get; set; }
        public int contactNo { get; set; }

    }
}