// File:    DBPeriods.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class DBPeriods

using System;
namespace ITDB.Models
{
    /// 夺宝
    public class DBPeriods
    {
        /// <summary>
        /// 主键ID
        /// <summary>
        public int ID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// int
        /// </summary>
        public int PeriodsCode { get; set; }
        /// <summary>
        /// 需要次数
        /// </summary>
        public int NeedNum { get; set; }
        /// <summary>
        /// 商品价格(每期价格可能浮动)
        /// </summary>
        public decimal GoodsPrice { get; set; }
        /// <summary>
        /// 价格/人次
        /// </summary>
        public decimal PerPrice { get; set; }
        /// <summary>
        /// 剩余人次
        /// </summary>
        public int OverplusNum { get; set; }
        /// <summary>
        /// 是否开奖
        /// </summary>
        public bool IfOpen { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// 待开奖时间
        /// </summary>
        public DateTime? WaitOpenTime { get; set; }
        /// <summary>
        /// 中奖用户
        /// </summary>
        public int? LuckyUserID { get; set; }
        /// <summary>
        /// 中奖号码
        /// </summary>
        public int? LuckyCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 0 进行中 1正在开奖中2开奖成功3开奖失败
        /// </summary>
        public int Status { get; set; }
        

    }
}