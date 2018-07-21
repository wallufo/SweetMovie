using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SweetMoive.Areas.AdminPerson.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(30,MinimumLength =4,ErrorMessage ="{0}长度为{2}-{1}个字符")]
        [Display(Name ="账号")]
        public string Accounts { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="{0}长度为{2}-{1}个字符")]
        [Display(Name ="密码")]
        public string Password { get; set; }
    }
}