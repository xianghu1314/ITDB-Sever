// File:    Category.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Category

using System;
namespace ITDB.Models
{
    /// 商品分类
    public class Category
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 父栏目ID
        /// </summary>
        public int? ParentCategoryID { get; set; }
        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }  
        /// <summary>
        /// Logo
        /// </summary>
        public string BannerLogo { get; set; }
        /// <summary>
        /// 栏目名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

    }
}