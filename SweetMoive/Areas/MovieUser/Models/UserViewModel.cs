using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SweetMoive.Areas.MovieUser.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings =false)]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        [Display(Name="甜值")]
        public int SweetScore { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "呢称")]
        public string Name { get; set; }
        [Display(Name ="性别")]
        public string Sex { get; set; }
        [Display(Name ="个性签名")]
        public string Mymottoy { get; set; }
        [Display(Name="角色")]
        public string Role { get; set; }
    }
}