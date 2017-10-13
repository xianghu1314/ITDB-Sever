// File:    ShowOrder.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class ShowOrder

using System;
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
        public int DBPeriodsID { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img1 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img6 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img5 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img4 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img3 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img2 { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public int UserID { get; set; }

    }
}