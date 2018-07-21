using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Display(Name ="用户ID")]
        public int UserID { get; set; }
        [Display(Name ="电影ID")]
        public int MovieID { get; set; }
        [Display(Name ="评论内容")]
        public string Content { get; set; }
        [Display(Name ="评论时间")]
        public DateTime? CommentTime { get; set; }
        [Display(Name ="用户评分")]
        public float Score { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
