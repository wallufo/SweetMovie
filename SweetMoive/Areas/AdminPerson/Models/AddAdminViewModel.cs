using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Models
{
    public class AddAdminViewModel
    {
        [Remote("CanAccounts", "Admin", HttpMethod = "Post", ErrorMessage = "账户已存在")]
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(30,MinimumLength =6,ErrorMessage ="{0}长度为{2}-{1}个字符")]
        [Display(Name ="账号")]
        public string Accounts { get; set; }
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(256,MinimumLength =6,ErrorMessage ="{0}长度少于{1}个字符")]
        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}