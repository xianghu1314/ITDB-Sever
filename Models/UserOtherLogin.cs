// File:    UserOtherLogin.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class UserOtherLogin

using System;
using System.ComponentModel.DataAnnotations;

namespace ITDB.Models
{
    public class UserOtherLogin
    {
        /// <summary>
        /// 主键ID
        /// <summary>
        public int ID { get; set; }
        /// <summary>
        /// 关联用户ID
        /// </summary>
        [Required]
        public int UserID { get; set; }
        /// <summary>
        /// </summary>
        [StringLength(36)]
        public string Key1 { get; set; }
        /// <summary>
        /// </summary>
        [StringLength(36)]
        public string Key2 { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(255)]
        public string icon { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string nickname { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        [StringLength(36)]
        public string Code { get; set; }

    }
}