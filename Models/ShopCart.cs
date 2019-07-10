// File:    ShopCart.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class ShopCart

using System;
using System.ComponentModel.DataAnnotations;

namespace ITDB.Models
{
    public class ShopCart
    {
        ///<summary>
        ///主键
        ///</summary>
        public int ID { get; set; }
        ///<summary>
        ///夺宝期数ID
        ///</summary>
        public int DBPeriodsID { get; set; }
        ///<summary>
        ///数量
        ///</summary>
        public int Num { get; set; }
        ///<summary>
        ///单价
        ///</summary>
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        ///<summary>
        ///用户
        ///</summary>
        public int UserID { get; set; }
        ///<summary>
        ///商品ID 冗余
        ///</summary>
        public int GoodsID { get; set; }
    }

}