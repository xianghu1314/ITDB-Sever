// File:    Order.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Order

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITDB.Models
{
    /// 订单
    public class Order
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(36)]
        [Required]
        public string OrderCode { get; set; }
        /// <summary>
        /// 是否支付
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool IfPay { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Required]
        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime SubmitTime { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        [DataType(DataType.Currency)]
        [Required]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        [Required]
        public int UserID { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        [StringLength(20)]
        [Required]
        public string PostUserName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        [StringLength(11)]
        [Required]
        public string PostUserPhone { get; set; }
        /// <summary>
        /// 邮寄地址
        /// </summary>
        [StringLength(255)]
        [Required]
        public string PostAddress { get; set; }
        /// <summary>
        /// 邮寄详细地址
        /// </summary>
        [StringLength(255)]
        public string PostDetailAddress { get; set; }
        /// <summary>
        /// 订单状态(0待支付，100待发货（已付款），200采购中，300已发货，400已收货，500待评价，600已完成)
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int OrderStatus { get; set; }
        /// <summary>
        /// </summary>
        [StringLength(20)]
        [Required]
        public string IpAddress { get; set; }
        /// <summary>
        /// </summary>
        [StringLength(50)]
        [Required]
        public string IpCity { get; set; }

    }
}