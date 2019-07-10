// File:    OrderDetail.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class OrderDetail

using System;
using System.ComponentModel.DataAnnotations;

namespace ITDB.Models
{
    /// 订单详细
    public class OrderDetail
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderID { get; set; }
        /// <summary>
        /// 商品本期ID
        /// </summary>
        public int DBPeriodsID { get; set; }
        /// <summary>
        /// 购买期数
        /// </summary>
        public int DBNum { get; set; }
        /// <summary>
        /// 价格/人次
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal DBPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal DBPerPrice { get; set; }

    }
}