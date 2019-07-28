// File:    Category.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Category

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITDB.Models
{
    /// <summary>
    /// 栏目配置
    /// </summary>
    public class ConfigureCategory
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [DefaultValue(false)]
        [Required]
        public bool IfShow { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DefaultValue(0)]
        [Required]
        public int Sort { get; set; }
        /// <summary>
        /// 参数【分类ID或者其他】
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Data { get; set; }
        /// <summary>
        /// 连接 跳转到url
        /// </summary>
        [StringLength(255)]
        public string Url { get; set; }

    }
}