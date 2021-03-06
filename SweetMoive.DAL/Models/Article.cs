﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.Models
{
    /// <summary>
    /// 影评文章模型类
    /// </summary>
    public class Article
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int MovieID { get; set; }
        [Display(Name ="发布时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Releasetime { get; set; }
        [Display(Name ="标题")]
        public string Title { get; set; }
        [Display(Name ="内容")]
        public string Content { get; set; }
        public enum Status
        {
            审核不通过,审核通过, 待审核,
        }
        /// <summary>
        /// 【0】不通过,【1】通过,【2】进行中
        /// </summary>
        [Display(Name ="审核状态")]
        public Status Auditstatus { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }

    }
}
