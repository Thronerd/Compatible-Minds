using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Admin
    {
        [Key]
        public string AdminId { get; set; }
        public string name { get; set; }
        public string IdNo { get; set; }
        public string contactNo { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}