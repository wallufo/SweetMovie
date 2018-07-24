using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    /// <summary>
    /// 用户关注模型类
    /// </summary>
    public class Follower
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int Followed_user { get; set; }
    }
}
