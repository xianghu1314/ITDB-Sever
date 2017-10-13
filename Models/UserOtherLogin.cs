// File:    UserOtherLogin.cs
// Author:  Administrator
// Created: 2017年9月27日 16:46:25
// Purpose: Definition of Class UserOtherLogin

using System;
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
        public int UserID { get; set; }
        /// <summary>
        /// </summary>
        public string Key1 { get; set; }
        /// <summary>
        /// </summary>
        public string Key2 { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string Code { get; set; }

    }
}