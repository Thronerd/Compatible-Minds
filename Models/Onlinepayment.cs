using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public  class OnlinePayment
    {
        [Key]
        public string RefNo { get; set; }

        public string cardType { get; set; }
        public string cardholderName { get; set; }
        public string cardNumber { get; set; }
        public string ccvno { get; set; }
        public string expiryDate { get; set; }
        public double amount { get; set; }
    }
}
