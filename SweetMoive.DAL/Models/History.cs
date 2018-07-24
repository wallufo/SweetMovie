using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    public class History
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        [Display(Name ="浏览时间")]
        public Nullable<DateTime> ViewTime { get; set; }
    }
}
