using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHC.Portal.Extensions;

namespace YHC.Portal.Models
{
    public class UserBank
    {/// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public Bank Bank { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public String Province { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// 支行名称
        /// </summary>
        public String BranchName { get; set; }

        /// <summary>
        /// 开户人
        /// </summary>
        public String AccountName { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public String AccountNo { get; set; }

        /// <summary>
        /// 银行账号格式化，例如：1234 3434 3455 6
        /// </summary>
        public String AccountNoFormat
        {
            get
            {
                return this.AccountNo.BankFormat();
            }
        }
    }
}