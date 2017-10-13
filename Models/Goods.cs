// File:    Goods.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Goods

using System;
namespace ITDB.Models
{
    /// 商品表
    public class Goods
    {
        /// <summary>
        /// 商品id
        /// <summary>
        public int ID { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsDescribe { get; set; }
        /// <summary>
        /// 商品Logo
        /// </summary>
        public string GoodsLogo2 { get; set; }
        /// <summary>
        /// 商品Logo
        /// </summary>
        public string GoodsLogo { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodPrice { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IfShow { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        public string GoodsDetail { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}