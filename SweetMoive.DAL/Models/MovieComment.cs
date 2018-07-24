using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    public class MovieComment
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        [Display(Name ="评论内容")]
        public string Content { get; set; }
        [Display(Name ="评论时间")]
        public DateTime? CommentTime { get; set; }
        [Display(Name ="用户评分")]
        public float Score { get; set; }
        [Display(Name ="点赞数")]
        public int Like { get; set; }
    }
}
