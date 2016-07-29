using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Application
    {
        public int AppID { get; set; }
        public string studentNo { get; }

        public virtual Student Student { get; set; }
    }
}
