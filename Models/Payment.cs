using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Models
{
   public class Payment
    {
        [Key]
        [Display(Name = "Bank receipt no")]
        public string recieptNo { get; set; }
       
        public double amountPaid { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

       
    }
}
