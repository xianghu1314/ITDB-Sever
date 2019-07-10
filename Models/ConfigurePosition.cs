// File:    Category.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class Category

using System;
using System.ComponentModel.DataAnnotations;

namespace ITDB.Models
{
    /// <summary>
    /// 位置配置
    /// </summary>
    public class ConfigurePosition
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }
        

    }
}