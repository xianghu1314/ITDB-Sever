// File:    Category.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Category

using System;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(255)]
        [Required]
        public string Logo { get; set; }
        /// <summary>
        /// BannerLogo
        /// </summary>
        [StringLength(255)]
        public string BannerLogo { get; set; }
        /// <summary>
        /// 栏目名
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

    }
}