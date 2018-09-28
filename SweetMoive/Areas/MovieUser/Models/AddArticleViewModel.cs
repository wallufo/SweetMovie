using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SweetMoive.Areas.MovieUser.Models
{
    public class AddArticleViewModel
    {
        [Required(ErrorMessage = "必须选择{0}")]
        [Display(Name = "电影名称")]
        public int MovieID { get; set; }
        [Required(ErrorMessage = "必须选择{0}")]
        [Display(Name = "发布时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Releasetime { get; set; }
        [Display(Name = "标题")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string Content { get; set; }
    }
}