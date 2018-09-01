using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SweetMoive.Areas.MovieUser.Models
{
    public class LoginViewModel
    {
        [Display(Name ="用户名")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="密码")]
        public string Password { get; set; }
    }
}