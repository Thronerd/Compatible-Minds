using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Funder
    {
        [Key]
        public int funderId { get; set; }
        public string funderName { get; set; }
    }
}