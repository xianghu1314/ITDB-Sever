// File:    ShowOrder.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class ShowOrder

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITDB.Models
{
    /// 晒单
    public class ShowOrder
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 商品本期ID
        /// </summary>
        [Required]
        public int DBPeriodsID { get; set; }
        /// <summary>
        /// 图片s
        /// </summary>
        [StringLength(1000)]
        public string images { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        public int UserID { get; set; }

    }
}