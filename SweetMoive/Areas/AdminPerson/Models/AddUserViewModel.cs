using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Models
{
    public class AddUserViewModel
    {
        public enum Roles
        {
            评论专家, 普通用户
        }
        [Required(ErrorMessage ="必须选择{0}")]
        [Display(Name ="用户角色")]
        public Roles Role { get; set; }
        [Remote("HasUsername", "User", HttpMethod = "Post", ErrorMessage = "用户名已存在")]
        [Required(ErrorMessage ="必须输入{0}")]
        [Display(Name ="用户名")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "{0}长度{2}-{1}个字符")]
        public string Username { get; set; }
        [Remote("HasEmailAdress","User",HttpMethod ="Post",ErrorMessage ="邮箱已存在")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮箱")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string EmailAdress { get; set; }
        [Display(Name = "昵称")]
        [StringLength(12,MinimumLength =4,ErrorMessage = "{0}长度{2}-{1}个字符")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "{0}长度{2}-{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "甜值")]
        public int SweetScore { get; set; }
        public enum UserStatus
        {
            未启用, 启用
        }
        [Display(Name = "账号状态")]
        [Required(ErrorMessage ="必须选择{0}")]
        public UserStatus Userstatus { get; set; }
    }
}