using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Models
{
    public class AddCommentViewModel
    {
        [Required(ErrorMessage ="必须选择{0}")]
        [Display(Name ="用户名")]
        public int UserID { get; set; }
        [Display(Name = "电影名称")]
        [Required(ErrorMessage = "必须选择{0}")]
        public int MovieID { get; set; }
        [Display(Name = "评论内容")]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "评论时间")]
        [Required(ErrorMessage = "必须选择{0}")]
        public DateTime? CommentTime { get; set; }
        [Display(Name = "用户评分")]
        [Range(1, 10,ErrorMessage ="{0}必须在{1}-{2}范围内")]
        [Required(ErrorMessage = "必须输入{0}")]
        public float Score { get; set; }
    }
}