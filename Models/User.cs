// File:    User.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class User

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITDB.Models
{
    /// 用户
    public class User
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [StringLength(50)]
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [StringLength(255)]
        public string UserLogo { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        [StringLength(11)]
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户余额
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal UserBalance { get; set; }
        /// <summary>
        /// 账户锁定
        /// </summary>
        
        public bool Status { get; set; }

    }
}