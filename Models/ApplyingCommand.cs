using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplyingCommand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplyingYear { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ApplyingO_Date { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ApplyingC_Date { get; set; }
        public string Semester { get; set; }
    }
}
