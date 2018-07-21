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
        public int UserID { get; set; }
        [StringLength(30, MinimumLength = 6, ErrorMessage = "{0}长度{2}-{1}个字符")]
        [Display(Name = "用户名")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 6, ErrorMessage ="{0}长度{2}-{1}个字符")]
        [Display(Name ="密码")]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name ="电子邮箱")]
        public string EmailAdress { get; set; }
        [Display(Name="昵称")]
        [StringLength(15,MinimumLength =6,ErrorMessage ="{0}长度{2}-{1}个字符")]
        public string Name { get; set; }
        public enum Roles
        {
            评论专家,普通用户
        }
        [Display(Name ="用户角色")]
        public Roles Role { get; set; }
        [Display(Name ="头像")]
        public string DefaultImgUrl { get; set; }
        [Display(Name ="性别")]
        [Range(0,1,ErrorMessage ="{0}范围{1}-{2}")]
        public int Sex { get; set; }
    }
}
