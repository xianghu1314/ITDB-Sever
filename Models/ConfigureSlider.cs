// File:    Category.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Category

using System;
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
        public string Name { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IfShow { get; set; }  
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public int PositionID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

    }
}