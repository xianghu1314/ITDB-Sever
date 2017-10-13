// File:    DBDetail.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class DBDetail

using System;
namespace ITDB.Models
{
    public class DBOrderDetail
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid OrderDetailID { get; set; }
        /// <summary>
        /// 商品本期ID
        /// </summary>
        public int DBPeriodsID { get; set; }
        /// <summary>
        /// 夺宝号码
        /// </summary>
        public int DBTicket { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public int UserID { get; set; }

    }
}