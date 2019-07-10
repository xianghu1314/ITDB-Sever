// File:    ShippingAddress.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class ShippingAddress

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITDB.Models
{
    /// 收货地址
    public class ShippingAddress
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 默认
        /// </summary>
        
        public bool IfDefault { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        [StringLength(50)]
        public string UserPhone { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(255)]
        public string DetailAddress { get; set; }

    }
}