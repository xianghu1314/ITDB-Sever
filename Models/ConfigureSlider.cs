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
    /// 轮播配置
    /// </summary>
    public class ConfigureSlider
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
        [Required]
        [DefaultValue(false)]
        public bool IfShow { get; set; }  
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int Sort { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        [Required]
        public int PositionID { get; set; }
        /// <summary>
        /// 跳转到url
        /// </summary>
        [StringLength(255)]
        public string Url { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [StringLength(255)]
        public string ImgUrl { get; set; }

    }
}