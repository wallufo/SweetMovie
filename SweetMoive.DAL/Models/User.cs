using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "{0}长度{2}-{1}个字符")]
        [Display(Name = "用户名")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(256, MinimumLength = 6, ErrorMessage ="{0}长度{2}-{1}个字符")]
        [Display(Name ="密码")]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name ="电子邮箱")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string EmailAdress { get; set; }
        [Display(Name="昵称")]
        public string Name { get; set; }
        public enum Roles
        {
            评论专家,普通用户
        }
        [Display(Name ="用户角色")]
        public Roles? Role { get; set; }
        [Display(Name="座右铭")]
        public string MyMotto { get; set; }
        [Display(Name ="头像")]
        public string DefaultImgUrl { get; set; }
        [Range(0,100)]
        [Display(Name ="甜值")]
        public int SweetScore { get; set; }
        public enum Sexs
        {
            男,女
        }
        [Display(Name ="性别")]
        public Sexs? Sex { get; set; }
        public enum UserStatus
        {
            未启用,启用
        }
        [Display(Name ="账号状态")]
        [Required(ErrorMessage = "必须选择{0}")]
        public UserStatus Userstatus { get; set; }
    }
}
