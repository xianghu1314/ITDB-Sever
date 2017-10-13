// File:    ShippingAddress.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class ShippingAddress

using System;
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
        public string UserPhone { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }

    }
}