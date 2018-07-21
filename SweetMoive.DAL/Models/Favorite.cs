using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    public class Favorite
    {
        [Key]
        public int FavoriteID { get; set; }
        [Display(Name ="用户ID")]
        public int UserID { get; set; }
        [Display(Name ="电影ID")]
        public int MovieID { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
