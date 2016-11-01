using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using YHC.Portal.Extensions;


namespace YHC.Portal.Models
{
    [Serializable]
    public class AdminBank
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public Bank Bank { get; set; }

        public String BankName
        {
            get { return Bank == null ? string.Empty : Bank.BankName; }
        }

        /// <summary>
        /// 开户人
        /// </summary>
        public String AccountName { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public String AccountNo { get; set; }

        public String AccountNoFormat
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.AccountNo))
                {
                    return "";
                }
                // 如果不是数字格式，则不格式化
                Regex regex = new Regex("^[0-9]*$");
                if (regex.IsMatch(this.AccountNo))
                {
                    return this.AccountNo.BankFormat();
                }

                return this.AccountNo;
            }
        }

        public String AccountNoShow
        {
            get
            {
                return string.IsNullOrEmpty(AccountNo) ? "" : AccountNo.BankCardNoAsteriskText();
            }
        }

        /// <summary>
        /// 开户行
        /// </summary>
        public String OpeningBank { get; set; }
    }
}
