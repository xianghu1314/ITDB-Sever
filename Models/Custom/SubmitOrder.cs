using System;
using System.Collections.Generic;

namespace ITDB.Models.Custom{
    public class SubmitOrder{
        public int[] ShopCartID { get; set; }
        public int AddressID { get; set; }
        ///<summary>
        ///0、1、2 余额、微信、支付宝
        ///<summary>
        public int PayMode { get; set; }
    }
}