// File:    DBDetail.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class DBDetail

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public Guid OrderDetailID { get; set; }
        /// <summary>
        /// 商品本期ID
        /// </summary>
        [Required]
        public int DBPeriodsID { get; set; }
        /// <summary>
        /// 夺宝号码
        /// </summary>
        [Required]
        public int DBTicket { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        public int UserID { get; set; }

    }
}