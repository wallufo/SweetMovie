using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static SweetMoive.DAL.Models.User;

namespace SweetMoive.Areas.MovieUser.Models
{
    public class EditUserViewModel
    {
        public int ID { get; set; }
        [Display(Name="呢称")]
        public string Name { get; set; }
        [Display(Name="个性签名")]
        public string Mottoy { get; set; }
        [Display(Name="性别")]
        public Sexs? Sex { get; set; }
        [Display(Name="头像")]
        public string DefalutImgUrl { get; set; }
    }
}