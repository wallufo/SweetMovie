using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SweetMoive.Areas.AdminPerson.Models
{
    public class AddArticelViewModel
    {
        [Required(ErrorMessage = "必须选择{0}")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "必须选择{0}")]
        public int MovieID { get; set; }
        [Required(ErrorMessage ="必须选择{0}")]
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
        public enum Status
        {
            审核不通过, 审核通过, 待审核,
        }
        /// <summary>
        /// 【0】不通过,【1】通过,【2】进行中
        /// </summary>
        [Display(Name = "审核状态")]
        public Status Auditstatus { get; set; }
    }
}