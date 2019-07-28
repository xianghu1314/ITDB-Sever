using System.ComponentModel.DataAnnotations;

namespace ITDB.Models.Custom
{
    public class request
    {
        ///状态
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        
    }
    public class GetDBPeriodsRequest: request
    {
        public int CategoryId { get; set; }

    }
    public class UserLoginRequest
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        [StringLength(11)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string UserPwd { get; set; }

    }
}
