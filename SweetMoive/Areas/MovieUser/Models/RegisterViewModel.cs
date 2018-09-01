using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.MovieUser.Models
{
    public class RegisterViewModel
    {
        [Remote("HasUsername", "Home", "MovieUser", HttpMethod = "Post", ErrorMessage = "用户名已存在")]
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "用户名")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "{0}长度{2}-{1}个字符")]
        public string Username { get; set; }
        [Remote("HasEmailAdress", "Home","MovieUser", HttpMethod = "Post", ErrorMessage = "邮箱已存在")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮箱")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string EmailAdress { get; set; }
        [Display(Name ="验证码")]
        public string Code { get; set; }
        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}