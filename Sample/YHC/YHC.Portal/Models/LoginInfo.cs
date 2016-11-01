using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    public class LoginInfo
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public string Token_Type { get; set; }
    }
}
