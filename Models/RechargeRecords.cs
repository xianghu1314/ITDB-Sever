// File:    RechargeRecords.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class RechargeRecords

using System;
namespace ITDB.Models
{
    /// 充值记录
    public class RechargeRecords
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string ChargeCode { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal ChargeMoney { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 充值账户
        /// </summary>
        public int UserID { get; set; }

    }
}