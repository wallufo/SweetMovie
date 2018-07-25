using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Models
{
    public class AddMovieViewModel
    {
        [Display(Name = "电影名称")]
        [Remote("HasMovieName","Movie",HttpMethod ="Post",ErrorMessage ="该电影已经存在")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string MovieName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "上映日期")]
        [Required(ErrorMessage = "必须输入{0}")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "演员")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string Actors { get; set; }
        [Display(Name = "剧情简介")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string Synopsis { get; set; }
        [Display(Name = "导演")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string Director { get; set; }
        public enum Types
        {
            剧情, 喜剧, 动作, 爱情, 科幻,
            悬疑, 惊悚, 恐怖, 犯罪, 同性,
            音乐, 歌舞, 传记, 历史, 战争,
            西部, 奇幻, 冒险, 灾难, 武侠,
            情色, 其他
        }
        [Display(Name = "类型")]
        [DisplayFormat(NullDisplayText = "无")]
        [Required(ErrorMessage = "必须选择{0}")]
        public Types? Type { get; set; }
        [Display(Name = "时长")]
        [Required(ErrorMessage = "必须输入{0}")]
        public int Duration { get; set; }
        [Display(Name = "国家")]
        [Required(ErrorMessage = "必须输入{0}")]
        public string Contry { get; set; }
        [Display(Name = "海报图片")]
        public int PostersNum { get; set; }
        [Display(Name = "封面图片")]
        public string DefaultImgUrl { get; set; }
        [Display(Name = "评分")]
        [Required(ErrorMessage = "必须输入{0}")]
        public float Score { get; set; }
        [Display(Name = "电影预告片")]
        [Required(ErrorMessage = "必须输入{0}")]
        [DataType(DataType.Url)]
        public string PreviewUrl { get; set; }
        public enum Hiddens
        {
            隐藏, 显示
        }
        [Display(Name = "是否隐藏")]
        public Hiddens Hidden { get; set; }
    }
}