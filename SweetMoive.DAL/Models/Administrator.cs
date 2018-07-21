using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }
        [Required(ErrorMessage ="必须输入{0}")]
        [Display(Name ="账号")]
        [StringLength(30,MinimumLength =6,ErrorMessage ="{0}长度{2}-{1}个字符")]

        public string Accounts { get; set; }
        [Required(ErrorMessage ="必须输入{0}")]
        [DataType(DataType.Password)]
        [StringLength(256,ErrorMessage ="{0}长度少于{1}字符")]
        [Display(Name ="密码")]
        public string Password { get; set; }
        [Display(Name ="登陆IP")]
        public string LoginIP { get; set; }
        [Display(Name ="登陆时间")]
        public DateTime? LoginTime { get; set; }
        [Display(Name ="创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
